namespace Domain.Interface.Generic
{
    public interface IGeneric<T> where T : class
    {
        Task<List<T>> Get();

        Task<T?> Get(int id);

        Task<T?> Add (T entity);

        Task<T?> Update (T entity);

        Task<T?> Delete (T entity);
    }
}
