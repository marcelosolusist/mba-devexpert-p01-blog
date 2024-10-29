using BlogExpert.Dados.Context;
using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlogExpert.Dados.Repository
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(BlogExpertDbContext context) : base(context) { }

        public override async Task<List<Autor>> Listar()
        {
            return Db.Autores.OrderBy(a => a.Email).ToList();
        }
    }
}
