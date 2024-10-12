using BlogExpert.Dados.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BlogExpert.Mvc.Configurations
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

            if (env.IsDevelopment() || env.IsStaging())
            {
                await context.Database.MigrateAsync();

                await EnsureSeedUsuarios(context);
                await EnsureSeedAutores(context);
            }
        }

        private static async Task EnsureSeedAutores(BlogExpertDbContext context)
        {
            if (context.Autores.Any())
                return;
        }

        private static async Task EnsureSeedUsuarios(BlogExpertDbContext context)
        {
            if (context.Users.Any())
                return;

            var idUsuario = "56c4c8d7-baf9-4044-9220-475ea873a262";

            await context.Users.AddAsync(new IdentityUser
            {
                Id = idUsuario,
                UserName = "blogexpert@blogexpert.com",
                NormalizedUserName = "BLOGEXPERT@BLOGEXPERT.COM",
                Email = "blogexpert@blogexpert.com",
                NormalizedEmail = "BLOGEXPERT@BLOGEXPERT.COM",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEBUFkipKfmhLc8SX3nwBA/9/B8zd9taBCG4XrTuksoWHhTr5FYfXZmolEbsPfz7f5A==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = "8d779be7-1d07-4bd5-accd-c7579503fbbd",
                EmailConfirmed = true,
                SecurityStamp = "GIHDMRZSWYYCZ56H3LHFNRSOO7NCFEUT"
            });

            await context.SaveChangesAsync();

            if (context.Roles.Any())
                return;

            var idRoleAdmin = "1";

            await context.Roles.AddAsync(new IdentityRole
            {
                Id = idRoleAdmin,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "8d779be7-1d07-4bd5-accd-c7579503fbbd"
            });

            await context.SaveChangesAsync();

            if (context.UserRoles.Any())
                return;

            await context.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = idRoleAdmin,
                UserId = idUsuario,
            });

            await context.SaveChangesAsync();
        }
    }
}
