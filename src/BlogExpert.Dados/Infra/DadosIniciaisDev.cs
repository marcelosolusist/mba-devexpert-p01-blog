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
                Email = "marcelo@solusist.com.br",
                EmailCriacao = "marcelo@solusist.com.br",
                DataCriacao = dataCriacaoComum
            });

            await context.Autores.AddAsync(new Autor
            {
                Id = idAutorMayane,
                Nome = "Mayane Menezes",
                Ativo = true,
                Email = "mayane@solusist.com.br",
                EmailCriacao = "mayane@solusist.com.br",
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
                EmailCriacao = "marcelo@solusist.com.br",
                DataCriacao = DateTime.Now
            });

            await context.Posts.AddAsync(new Post
            {
                Id = idPostMayane,
                AutorId = idAutorMayane,
                Titulo = "É preciso saber viver",
                Descricao = "Quem espera que a vida seja feita de ilusão pode até ficar maluco ou morrer na solidão.",
                EmailCriacao = "mayane@solusist.com.br",
                DataCriacao = DateTime.Now
            });

            await context.SaveChangesAsync();

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMarcelo,
                Descricao = "Concordo em gênero, número e grau.",
                EmailCriacao = "mayane@solusist.com.br",
                DataCriacao = DateTime.Now
            });

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMarcelo,
                Descricao = "Sem planejamento não dá pra viver.",
                EmailCriacao = "blogexpert@blogexpert.com",
                DataCriacao = DateTime.Now
            });

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMayane,
                Descricao = "Toda pedra no caminho você pode retirar.",
                EmailCriacao = "marcelo@solusist.com.br",
                DataCriacao = DateTime.Now
            });

            await context.Comentarios.AddAsync(new Comentario
            {
                Id = Guid.NewGuid(),
                PostId = idPostMayane,
                Descricao = "Numa flor que tem espinhos você pode se arranhar.",
                EmailCriacao = "blogexpert@blogexpert.com",
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
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
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

            await context.Users.AddAsync(new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "marcelo@solusist.com.br",
                NormalizedUserName = "MARCELO@SOLUSIST.COM.BR",
                Email = "marcelo@solusist.com.br",
                NormalizedEmail = "MARCELO@SOLUSIST.COM.BR",
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
                UserName = "mayane@solusist.com.br",
                NormalizedUserName = "MAYANE@SOLUSIST.COM.BR",
                Email = "mayane@solusist.com.br",
                NormalizedEmail = "MAYANE@SOLUSIST.COM.BR",
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
