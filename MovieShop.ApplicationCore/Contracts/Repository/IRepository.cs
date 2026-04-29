namespace MovieShop.ApplicationCore.Contracts.Repository;

public interface IRepository<T> where T : class
{
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<T> Add(T entity);
    Task<T> AddAsync(T entity);
    Task<T> Update(T entity);
    Task Delete(T entity);
}
