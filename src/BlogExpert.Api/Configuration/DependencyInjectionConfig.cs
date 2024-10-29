using BlogExpert.Dados.Context;
using BlogExpert.Dados.Repository;
using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Notificacoes;
using BlogExpert.Negocio.Services;

namespace BlogExpert.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencias(this WebApplicationBuilder builder)
        {
            builder.Services.ResolveDependencias();

            return builder;
        }
        private static IServiceCollection ResolveDependencias(this IServiceCollection services)
        {
            services.AddScoped<BlogExpertDbContext>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IComentarioService, ComentarioService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IContaAutenticada, ContaAutenticada>();

            return services;
        }
    }
}
