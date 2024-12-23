﻿namespace Application.Interface.Generic
{
    public interface IGenericApplication<T> where T : class
    {
        Task<List<T>> Get();

        Task<T?> Get(int id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);
    }
}
