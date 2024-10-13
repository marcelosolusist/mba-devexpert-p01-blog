using BlogExpert.Negocio.Entities;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IComentarioService : IDisposable
    {
        Task Adicionar(Comentario comentario);
        Task Atualizar(Comentario comentario);
        Task Remover(Guid id);
        Task<Comentario> ObterParaEdicao(Guid id);
    }
}
