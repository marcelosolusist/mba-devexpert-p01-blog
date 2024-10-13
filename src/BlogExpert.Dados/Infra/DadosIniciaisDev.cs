using BlogExpert.Dados.Context;
using BlogExpert.Negocio.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogExpert.Dados.Infra
{
    public static class DadosIniciaisDev
    {
        public static async Task IncluirDadosIniciais(BlogExpertDbContext context)
        {
            await IncluirUsuarios(context);
            await IncluirAutoresPostsComentarios(context);
        }
        private static async Task IncluirAutoresPostsComentarios(BlogExpertDbContext context)
        {
            if (context.Autores.Any())
                return;

            var idAutorMarcelo = Guid.NewGuid();
            var idAutorMayane = Guid.NewGuid();
            var dataCriacaoComum = DateTime.Now;

            await context.Autores.AddAsync(new Autor
            {
                Id = idAutorMarcelo,
                Nome = "Marcelo Menezes",
                Ativo = true,
                Email = "marcelo@be.net",
                EmailCriacao = "marcelo@be.net",
                DataCriacao = dataCriacaoComum
            });

            await context.Autores.AddAsync(new Autor
            {
                Id = idAutorMayane,
                Nome = "Mayane Menezes",
                Ativo = true,
                Email = "mayane@be.net",
                EmailCriacao = "mayane@be.net",
                DataCriacao = dataCriacaoComum
            });

            await context.SaveChangesAsync();

            var idPostMarcelo = Guid.NewGuid();
            var idPostMayane = Guid.NewGuid();

            await context.Posts.AddAsync(new Post
            {
                Id = idPostMarcelo,
                AutorId = idAutorMarcelo,
                Titulo = "Importância do planejamento",
                Descricao = "Planejar é uma atividade essencial para o nosso dia a dia. Já dizia um velho sábio que para quem não sabe para onde quer ir todos os caminhos levam a lugar nenhum.",
                EmailCriacao = "marcelo@be.net",
                DataCriacao = DateTime.Now
            });

            await context.Posts.AddAsync(new Post
            {
                Id = idPostMayane,
                AutorId = idAutorMayane,
                Titulo = "É preciso saber viver",
                Descricao = "Quem espera que a vida seja feita de ilusão pode até ficar maluco ou morrer na solidão.",
                EmailCriacao = "mayane@be.net",
                DataCriacao = DateTime.Now
            });

            await context.SaveChangesAsync();

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMarcelo,
                Descricao = "Concordo em gênero, número e grau.",
                EmailCriacao = "mayane@be.net",
                DataCriacao = DateTime.Now
            });

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMarcelo,
                Descricao = "Sem planejamento não dá pra viver.",
                EmailCriacao = "be@be.net",
                DataCriacao = DateTime.Now
            });

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMayane,
                Descricao = "Toda pedra no caminho você pode retirar.",
                EmailCriacao = "marcelo@be.net",
                DataCriacao = DateTime.Now
            });

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMayane,
                Descricao = "Numa flor que tem espinhos você pode se arranhar.",
                EmailCriacao = "be@be.net",
                DataCriacao = DateTime.Now
            });

            await context.SaveChangesAsync();
        }

        private static async Task IncluirUsuarios(BlogExpertDbContext context)
        {
            if (context.Users.Any())
                return;

            var idUsuarioAdmin = "56c4c8d7-baf9-4044-9220-475ea873a262";

            await context.Users.AddAsync(new IdentityUser
            {
                Id = idUsuarioAdmin,
                UserName = "admin@be.net",
                NormalizedUserName = "ADMIN@BE.NET",
                Email = "admin@be.net",
                NormalizedEmail = "ADMIN@BE.NET",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEBUFkipKfmhLc8SX3nwBA/9/B8zd9taBCG4XrTuksoWHhTr5FYfXZmolEbsPfz7f5A==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = "8d779be7-1d07-4bd5-accd-c7579503fbbd",
                EmailConfirmed = true,
                SecurityStamp = "GIHDMRZSWYYCZ56H3LHFNRSOO7NCFEUT"
            });

            await context.Users.AddAsync(new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "bet@be.net",
                NormalizedUserName = "BE@BE.NET",
                Email = "be@be.net",
                NormalizedEmail = "BE@BE.NET",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEBUFkipKfmhLc8SX3nwBA/9/B8zd9taBCG4XrTuksoWHhTr5FYfXZmolEbsPfz7f5A==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = "8d779be7-1d07-4bd5-accd-c7579503fbbd",
                EmailConfirmed = true,
                SecurityStamp = "GIHDMRZSWYYCZ56H3LHFNRSOO7NCFEUT"
            });

            await context.Users.AddAsync(new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "marcelo@be.net",
                NormalizedUserName = "MARCELO@BE.NET",
                Email = "marcelo@be.net",
                NormalizedEmail = "MARCELO@BE.NET",
                AccessFailedCount = 0,
                LockoutEnabled = false,
                PasswordHash = "AQAAAAIAAYagAAAAEBUFkipKfmhLc8SX3nwBA/9/B8zd9taBCG4XrTuksoWHhTr5FYfXZmolEbsPfz7f5A==",
                TwoFactorEnabled = false,
                ConcurrencyStamp = "8d779be7-1d07-4bd5-accd-c7579503fbbd",
                EmailConfirmed = true,
                SecurityStamp = "GIHDMRZSWYYCZ56H3LHFNRSOO7NCFEUT"
            });

            await context.Users.AddAsync(new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "mayane@be.net",
                NormalizedUserName = "MAYANE@BE.NET",
                Email = "mayane@be.net",
                NormalizedEmail = "MAYANE@BE.NET",
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
                UserId = idUsuarioAdmin,
            });

            await context.SaveChangesAsync();
        }
    }
}
