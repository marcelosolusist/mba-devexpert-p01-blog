using BlogExpert.Negocio.Interfaces;
using System.Security.Claims;

namespace BlogExpert.Mvc.Configurations
{
    public class ContaAutenticada : IContaAutenticada
    {
        private readonly IHttpContextAccessor _accessor;

        public ContaAutenticada(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Email => _accessor.HttpContext.User.Identity.Name;

        public bool EstaAutenticada => _accessor.HttpContext.User.Identity.IsAuthenticated;

        public bool EhAdministrador => _accessor.HttpContext.User.IsInRole("Admin");
    }
}
