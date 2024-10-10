using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Seguranca;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IAutorService : IDisposable
    {
        Task Adicionar(Autor autor, ContaAutenticada contaAutenticada);
        Task Atualizar(Autor autor, ContaAutenticada contaAutenticada);
        Task Remover(Guid id, ContaAutenticada contaAutenticada);
    }
}
