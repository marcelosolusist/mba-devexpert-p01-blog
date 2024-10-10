using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Seguranca;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IPostService : IDisposable
    {
        Task Adicionar(Post post, ContaAutenticada contaAutenticada);
        Task Atualizar(Post post, ContaAutenticada contaAutenticada);
        Task Remover(Guid id, ContaAutenticada contaAutenticada);
        Task<List<Autor>> ListarAutoresDaContaAutenticada(ContaAutenticada contaAutenticada);
    }
}
