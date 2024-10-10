using BlogExpert.Negocio.Entities;
using BlogExpert.Negocio.Seguranca;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IComentarioService : IDisposable
    {
        Task Adicionar(Comentario comentario, ContaAutenticada contaAutenticada);
        Task Atualizar(Comentario comentario, ContaAutenticada contaAutenticada);
        Task Remover(Guid id, ContaAutenticada contaAutenticada);
    }
}
