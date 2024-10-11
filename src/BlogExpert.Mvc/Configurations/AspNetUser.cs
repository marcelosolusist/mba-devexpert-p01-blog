using BlogExpert.Negocio.Interfaces;
using BlogExpert.Negocio.Seguranca;
using System.Security.Claims;

namespace BlogExpert.Mvc.Configurations
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        private bool EhAdmin()
        {
            return _accessor.HttpContext.User.IsInRole ("Admin");
        }

        public ContaAutenticada ObterContaAutenticada()
        {
            return new ContaAutenticada(_accessor.HttpContext.User.Identity.Name, EhAdmin());
        }
    }
}
