using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Seguranca;

namespace BlogExpert.Negocio.Services
{
    public class ComentarioService : ServiceBase, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;

        public ComentarioService(IComentarioRepository comentarioRepository, INotificador notificador) : base(notificador)
        {
            _comentarioRepository = comentarioRepository;
        }

        public async Task Adicionar(Comentario comentario, ContaAutenticada contaAutenticada)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            comentario.EmailCriacao = contaAutenticada.EmailConta;

            await _comentarioRepository.Adicionar(comentario);
        }

        public async Task Atualizar(Comentario comentario, ContaAutenticada contaAutenticada)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            if (!VerificarSePodeManipularComentario(comentario, contaAutenticada)) return;

            await _comentarioRepository.Atualizar(comentario);
        }

        public async Task Remover(Guid id, ContaAutenticada contaAutenticada)
        {
            var comentario = await _comentarioRepository.ObterPorId(id);

            if (comentario == null)
            {
                Notificar("Comentario não existe!");
                return;
            }

            if (!VerificarSePodeManipularComentario(comentario, contaAutenticada)) return;

            await _comentarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _comentarioRepository?.Dispose();
        }

        private bool VerificarSePodeManipularComentario(Comentario comentario, ContaAutenticada contaAutenticada)
        {
            if (contaAutenticada.EhAdministrador || comentario.EmailCriacao == contaAutenticada.EmailConta) return true;

            Notificar("A conta autenticada não pode manipular esse comentário.");
            return false;
        }
    }
}
