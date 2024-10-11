using BlogExpert.Dados.Context;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogExpert.Dados.Repository
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(BlogExpertDbContext context) : base(context) { }

        public async Task<bool> VerificarSePossuiPost(Guid id)
        {
            if (Db.Posts.FirstOrDefault(post => post.AutorId == id) != null) return true;

            return false;
        }
    }
}
