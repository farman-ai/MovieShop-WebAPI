using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MovieShopDbContext _dbContext;

    public Repository(MovieShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }
// Is method ko add karein build error hatane ke liye
    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    // GetById ko Task<T?> karein taaki interface se match ho (Warning Fix)
    public virtual async Task<T?> GetById(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
 
    public async Task<T> Add(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}