using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.Infrastructure.Data;
using MovieShop.ApplicationCore.Models; 
namespace MovieShop.Infrastructure.Repository;

public class PurchaseRepository :Repository<Purchase>, IPurchaseRepository
{


public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<IEnumerable<Purchase>> GetPurchasesByUserId(int userId)
    {
        // Yahan Include lagana zaroori hai image ke liye
        return await _dbContext.Purchases
            .Include(p => p.Movie) 
            .Where(p => p.UserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<TopPurchasedMovieModel>> GetTopPurchasedMovies(DateTime? fromDate, DateTime? toDate)
    {
        var query = _dbContext.Purchases.Include(p => p.Movie).AsQueryable();

        if (fromDate.HasValue)
            query = query.Where(p => p.PurchaseDateTime >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(p => p.PurchaseDateTime <= toDate.Value);

        return await query
            .GroupBy(p => new { p.MovieId, p.Movie.Title })
            .Select(g => new TopPurchasedMovieModel
            {
                MovieId = g.Key.MovieId,
                Title = g.Key.Title,
                TotalPurchases = g.Count()
            })
            .OrderByDescending(m => m.TotalPurchases)
            .Take(50)
            .ToListAsync();
    }
}