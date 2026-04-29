using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.Infrastructure.Data;
using MovieShop.ApplicationCore.Entities;

namespace MovieShop.Infrastructure.Repository;

public class CastRepository : Repository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Cast?> GetCastById(int id)
    {
        return await _dbContext.Casts
            .Include(c => c.MovieCasts)
                .ThenInclude(mc => mc.Movie)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}