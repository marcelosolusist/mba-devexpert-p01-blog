using BlogExpert.Negocio.Entities;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IAutorService : IDisposable
    {
        Task Adicionar(Autor autor);
        Task Atualizar(Autor autor);
        Task Remover(Guid id);
    }
}
