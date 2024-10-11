using BlogExpert.Negocio.Seguranca;
using System.Security.Claims;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
        ContaAutenticada ObterContaAutenticada();
    }
}
