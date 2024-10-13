using BlogExpert.Dados.Context;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogExpert.Dados.Repository
{
    public class ComentarioRepository : Repository<Comentario>, IComentarioRepository
    {
        public ComentarioRepository(BlogExpertDbContext context) : base(context) { }

        public override async Task<Comentario> ObterPorId(Guid id)
        {
            return await Db.Comentarios.Include(c => c.Post)
                                       .ThenInclude(p => p.Autor)
                                       .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
