using BlogExpert.Dados.Context;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;

namespace BlogExpert.Dados.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(BlogExpertDbContext context) : base(context) { }

        public async Task<bool> VerificarSePossuiComentario(Guid id)
        {
            if (Db.Comentarios.FirstOrDefault(comentario => comentario.PostId == id) != null) return true;

            return false;
        }
    }
}
