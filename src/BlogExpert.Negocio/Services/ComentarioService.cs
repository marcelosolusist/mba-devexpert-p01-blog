using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Negocio.Services
{
    public class ComentarioService : ServiceBase, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        private readonly IPostRepository _postRepository;

        public ComentarioService(IComentarioRepository comentarioRepository, IPostRepository postRepository, INotificador notificador, IContaAutenticada contaAutenticada) : base(notificador, contaAutenticada)
        {
            _comentarioRepository = comentarioRepository;
            _postRepository = postRepository;
        }

        public async Task Adicionar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            var comentarioDuplicado = _comentarioRepository.Buscar(c => c.Id == comentario.Id);
            if (comentarioDuplicado.Result.Any())
            {
                Notificar("Já existe um comentário com o Id informado.");
                return;
            }

            comentario.EmailCriacao = _contaAutenticada.Email;
            comentario.DataCriacao = DateTime.Now;

            if (!await VerificarSePostValidoEPodeManipularComentario(comentario))
            {
                return;
            }

            await _comentarioRepository.Adicionar(comentario);
        }

        public async Task Atualizar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            if (!await VerificarSePostValidoEPodeManipularComentario(await _comentarioRepository.ObterPorId(comentario.Id))) return;

            await _comentarioRepository.Atualizar(comentario);
        }

        public async Task Remover(Guid id)
        {
            var comentario = await _comentarioRepository.ObterPorId(id);

            if (comentario == null)
            {
                Notificar("Comentario não existe!");
                return;
            }

            if (!await VerificarSePostValidoEPodeManipularComentario(comentario)) return;

            await _comentarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _comentarioRepository?.Dispose();
        }

        public async Task<Comentario> ObterParaEdicao(Guid id)
        {
            var comentario = await _comentarioRepository.ObterPorId(id);

            if (comentario == null)
            {
                Notificar("Comentário não existe!");
                return null;
            }

            if (!await VerificarSePostValidoEPodeManipularComentario(comentario)) return null;

            return comentario;
        }

        private async Task<bool> VerificarSePostValidoEPodeManipularComentario(Comentario comentario)
        {
            var post = await _postRepository.ObterPorId(comentario.PostId);

            if (post == null)
            {
                Notificar("O post informado para o comentário não existe.");
                return false;
            }

            if (_contaAutenticada.EhAdministrador || comentario.EmailCriacao == _contaAutenticada.Email || comentario.Post.Autor.Email == _contaAutenticada.Email) return true;

            Notificar("A conta autenticada não pode manipular esse comentário.");
            return false;
        }
    }
}
