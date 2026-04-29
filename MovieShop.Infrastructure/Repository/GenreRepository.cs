using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.Infrastructure.Data;
using MovieShop.ApplicationCore.Entities;

namespace MovieShop.Infrastructure.Repository;

public class GenreRepository : Repository<Genre>, IGenreRepository
{
    public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Genre>> GetAllGenres()
    {
        return await _dbContext.Genres.ToListAsync();
    }

    public async Task<Genre?> GetGenreById(int id)
    {
        return await _dbContext.Genres
            .Include(g => g.MovieGenres)
            .ThenInclude(mg => mg.Movie)
            .FirstOrDefaultAsync(g => g.Id == id);
    }
}