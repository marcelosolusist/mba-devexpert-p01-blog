using BlogExpert.Negocio.Entities;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<bool> VerificarSePossuiComentario(Guid id);
    }
}
