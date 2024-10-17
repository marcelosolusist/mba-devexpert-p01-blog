using BlogExpert.Dados.Context;
using Microsoft.EntityFrameworkCore;

namespace BlogExpert.Api.Configuration
{
    public static class DbContextConfig
    {
        public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
        {
            //if (builder.Environment.IsDevelopment())
            //{
            //    builder.Services.AddDbContext<BlogExpertDbContext>(options =>
            //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionLite")));
            //}
            //else
            //{
                builder.Services.AddDbContext<BlogExpertDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            //}

            return builder;
        }
    }
}
