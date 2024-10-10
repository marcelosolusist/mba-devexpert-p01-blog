using BlogExpert.Dados.Repository;
using BlogExpert.Mvc.Data;
using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Notificacoes;
using BlogExpert.Negocio.Services;

namespace BlogExpert.Mvc.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IComentarioRepository, ComentarioRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IComentarioService, ComentarioService>();

            return services;
        }
    }
}
