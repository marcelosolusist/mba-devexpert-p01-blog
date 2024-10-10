using BlogExpert.Negocio.Entities;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IAutorRepository : IRepository<Autor>
    {
        Task<bool> VerificarSePossuiPost(Guid id);
    }
}
