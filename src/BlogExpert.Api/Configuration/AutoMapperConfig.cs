using AutoMapper;
using BlogExpert.Api.Models;
using BlogExpert.Negocio.Entities;

namespace BlogExpert.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Autor, AutorModel>().ReverseMap();
            CreateMap<Post, PostModel>().ReverseMap();
            CreateMap<Comentario, ComentarioModel>().ReverseMap();
        }
    }

    public static class AutoMapperAdd
    {
        public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return builder;
        }
    }
}
