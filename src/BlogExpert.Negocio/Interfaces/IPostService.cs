﻿using BlogExpert.Negocio.Entities;

namespace BlogExpert.Negocio.Interfaces
{
    public interface IPostService : IDisposable
    {
        Task Adicionar(Post post);
        Task Atualizar(Post post);
        Task Remover(Guid id);
    }
}
