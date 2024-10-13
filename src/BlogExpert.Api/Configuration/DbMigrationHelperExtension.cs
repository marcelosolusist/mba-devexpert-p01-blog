using BlogExpert.Dados.Context;
using BlogExpert.Dados.Infra;
using Microsoft.EntityFrameworkCore;

namespace BlogExpert.Api.Configuration
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }

    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var context = scope.ServiceProvider.GetRequiredService<BlogExpertDbContext>();

            if (env.IsDevelopment())
            {
                await context.Database.MigrateAsync();
                await DadosIniciaisDev.IncluirDadosIniciais(context);
            }
        }


    }
}
