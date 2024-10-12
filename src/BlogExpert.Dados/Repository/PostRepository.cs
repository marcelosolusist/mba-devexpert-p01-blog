using BlogExpert.Dados.Context;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public override async Task<Post> ObterPorId(Guid id)
        {
            return await Db.Posts.Include(p => p.Comentarios.OrderByDescending(c => c.DataCriacao)).FirstOrDefaultAsync(p => p.Id == id);  
        }

        public override async Task<List<Post>> Listar()
        {
            return await Db.Posts.Include(p => p.Autor)
                                 .Include(p => p.Comentarios.OrderByDescending(c => c.DataCriacao))
                                 .OrderByDescending(p => p.DataCriacao)
                                 .ToListAsync();
        }
    }
}
