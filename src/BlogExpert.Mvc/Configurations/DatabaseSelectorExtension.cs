using BlogExpert.Dados.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogExpert.Mvc.Configurations
{
    public static class DatabaseSelectorExtension
    {
        public static void AddDatabaseSelector(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BlogExpertDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
