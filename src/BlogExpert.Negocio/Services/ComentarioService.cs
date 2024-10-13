using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Negocio.Services
{
    public class ComentarioService : ServiceBase, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository, INotificador notificador, IContaAutenticada contaAutenticada) : base(notificador, contaAutenticada)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task Adicionar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            comentario.EmailCriacao = _contaAutenticada.Email;

            await _comentarioRepository.Adicionar(comentario);
        }

        public async Task Atualizar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            if (!VerificarSePodeManipularComentario(await _comentarioRepository.ObterPorId(comentario.Id))) return;

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

            if (!VerificarSePodeManipularComentario(comentario)) return;

            await _comentarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _comentarioRepository?.Dispose();
        }

        private bool VerificarSePodeManipularComentario(Comentario comentario)
        {
            if (_contaAutenticada.EhAdministrador || comentario.EmailCriacao == _contaAutenticada.Email || comentario.Post.Autor.Email == _contaAutenticada.Email) return true;

            Notificar("A conta autenticada não pode manipular esse comentário.");
            return false;
        }

        public async Task<Comentario> ObterParaEdicao(Guid id)
        {
            var comentario = await _comentarioRepository.ObterPorId(id);

            if (comentario == null)
            {
                Notificar("Comentário não existe!");
                return null;
            }

            if (!VerificarSePodeManipularComentario(comentario)) return null;

            return comentario;
        }
    }
}
