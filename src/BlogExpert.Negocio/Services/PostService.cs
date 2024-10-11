using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Seguranca;

namespace BlogExpert.Negocio.Services
{
    public class PostService : ServiceBase, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAutorRepository _autorRepository;
        public PostService(IPostRepository postRepository, IAutorRepository autorRepository, INotificador notificador) : base(notificador)
        {
            _postRepository = postRepository;
            _autorRepository = autorRepository;
        }
        public async Task Adicionar(Post post, ContaAutenticada contaAutenticada)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo).Result.Any())
            {
                Notificar("Já existe post com o título infomado.");
                return;
            }

            if (!await VerificarSeAutorValidoEPodeManipularPost(post, contaAutenticada, true))
            {
                return;
            }

            post.EmailCriacao = contaAutenticada.EmailConta;

            await _postRepository.Adicionar(post);
        }

        public async Task Atualizar(Post post, ContaAutenticada contaAutenticada)
        {
            if (!ExecutarValidacao(new PostValidation(), post)) return;

            if (_postRepository.Buscar(p => p.Titulo == post.Titulo && p.Id != post.Id).Result.Any())
            {
                Notificar("Já existe post com o título infomado.");
                return;
            }

            if (!await VerificarSeAutorValidoEPodeManipularPost(post, contaAutenticada, false)) return;

            await _postRepository.Atualizar(post);
        }

        public async Task Remover(Guid id, ContaAutenticada contaAutenticada)
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

            if (!await VerificarSeAutorValidoEPodeManipularPost(post, contaAutenticada, false)) return;

            await _postRepository.Remover(id);
        }

        public async Task<List<Autor>> ListarAutoresDaContaAutenticada(ContaAutenticada contaAutenticada)
        {

            if (!contaAutenticada.EhAdministrador) return _autorRepository.Buscar(a => a.Email == contaAutenticada.EmailConta).Result.ToList();

            return _autorRepository.Buscar(a => !string.IsNullOrEmpty(a.Nome)).Result.OrderBy(a => a.Nome).ToList();
        }

        public void Dispose()
        {
            _postRepository?.Dispose();
        }

        private async Task<bool> VerificarSeAutorValidoEPodeManipularPost(Post post, ContaAutenticada contaAutenticada, bool verificarAtivo)
        {
            var autor = await _autorRepository.ObterPorId(post.AutorId);

            if (autor == null)
            {
                Notificar("O autor informado para o post não existe.");
                return false;
            }

            if (verificarAtivo && !autor.Ativo)
            {
                Notificar("O autor não está ativo.");
                return false;
            }

            if (!contaAutenticada.EhAdministrador)
            {
                var autoresDaConta = await ListarAutoresDaContaAutenticada(contaAutenticada);
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

            if (verificarAtivo || contaAutenticada.EhAdministrador || post.EmailCriacao == contaAutenticada.EmailConta || autor.Email == contaAutenticada.EmailConta) return true;
            
            Notificar("A conta autenticada não pode manipular esse post.");
            return false;
        }

        
    }
}
