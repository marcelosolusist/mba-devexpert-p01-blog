using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Entities.Validations;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Negocio.Services
{
    public class ComentarioService : ServiceBase, IComentarioService
    {
        private readonly IComentarioRepository _comentarioRepository;
        public ComentarioService(IComentarioRepository comentarioRepository, INotificador notificador) : base(notificador)
        {
            _comentarioRepository = comentarioRepository;
        }
        public async Task Adicionar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

            await _comentarioRepository.Adicionar(comentario);
        }

        public async Task Atualizar(Comentario comentario)
        {
            if (!ExecutarValidacao(new ComentarioValidation(), comentario)) return;

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

            await _comentarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _comentarioRepository?.Dispose();
        }
    }
}
