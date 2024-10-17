﻿using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Negocio.Services
{
    public class PostService : ServiceBase, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAutorRepository _autorRepository;
        public PostService(IPostRepository postRepository, IAutorRepository autorRepository, INotificador notificador, IContaAutenticada contaAutenticada) : base(notificador, contaAutenticada)
        {
            _postRepository = postRepository;
            _autorRepository = autorRepository;
        }
        public async Task Adicionar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            var postDuplicado = _postRepository.Buscar(p => p.Id == post.Id);
            if (postDuplicado.Result.Any())
            {
                Notificar("Já existe um post com o Id informado.");
                return;
            }

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo).Result.Any())
            {
                Notificar("Já existe post com o título infomado.");
                return;
            }

            if (string.IsNullOrEmpty(post.AutorId.ToString()) || post.AutorId.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                Notificar("Autor não informado ou inválido.");
            }

            post.EmailCriacao = _contaAutenticada.Email;
            post.DataCriacao = DateTime.Now;

            await _postRepository.Adicionar(post);
        }

        public async Task Atualizar(Post post)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo && p.Id != post.Id).Result.Any())
            {
                Notificar("Já existe post com o título infomado.");
                return;
            }

            if (!await VerificarSeAutorValidoEPodeManipularPost(post)) return;

            await _postRepository.Atualizar(post);
        }

        public async Task Remover(Guid id)
        {
            var post = await _postRepository.ObterPorId(id);

            if (post == null)
            {
                Notificar("Post não existe!");
                return;
            }

            if (await _postRepository.VerificarSePossuiComentario(id))
            {
                Notificar("O post possui comentário cadastrado!");
                return;
            }

            if (!await VerificarSeAutorValidoEPodeManipularPost(post)) return;

            await _postRepository.Remover(id);
        }

        public async Task<Post> ObterParaEdicao(Guid id)
        {
            var post = await _postRepository.ObterPorId(id);

            if (post == null)
            {
                Notificar("Post não existe!");
                return null;
            }

            if (!_contaAutenticada.EhAdministrador && post.Autor.Email != _contaAutenticada.Email)
            {
                Notificar("A conta autenticada não pode manipular o post selecionado.");
                return null;
            }

            return post;
        }

        public async Task<List<Autor>> ListarAutoresDaContaAutenticada()
        {
            var listaAutores = new List<Autor>();
            if (!_contaAutenticada.EhAdministrador)
            {
                listaAutores = _autorRepository.Buscar(a => a.Email == _contaAutenticada.Email).Result.ToList();
            }
            else
            {
                listaAutores = _autorRepository.Buscar(a => !string.IsNullOrEmpty(a.Email)).Result.OrderBy(a => a.Email).ToList();
            }
            if (listaAutores.Count() <= 0)
            {
                Notificar("Apenas autores ou administradores podem manipular posts.");
            }
            return listaAutores;
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }

        private async Task<bool> VerificarSeAutorValidoEPodeManipularPost(Post post)
        {
            var autor = await _autorRepository.ObterPorId(post.AutorId);

            if (autor == null)
            {
                Notificar("O autor informado para o post não existe.");
                return false;
            }

            if (!_contaAutenticada.EhAdministrador)
            {
                var autoresDaConta = await ListarAutoresDaContaAutenticada();
                if (autoresDaConta == null)
                {
                    Notificar("Não há autor disponível para a conta autenticada.");
                    return false;
                }
                var autorInformado = autoresDaConta.FirstOrDefault(a => a.Id == post.AutorId);
                if (autorInformado == null)
                {
                    Notificar("A conta autenticada não pode manipular os posts do autor informado.");
                    return false;
                }
            }

            if (_contaAutenticada.EhAdministrador || post.EmailCriacao == _contaAutenticada.Email || autor.Email == _contaAutenticada.Email) return true;
            
            Notificar("A conta autenticada não pode manipular esse post.");
            return false;
        }

        
    }
}
