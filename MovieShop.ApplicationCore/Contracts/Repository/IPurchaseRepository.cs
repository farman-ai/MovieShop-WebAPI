using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;
namespace MovieShop.ApplicationCore.Contracts.Repository;

public interface IPurchaseRepository : IRepository<Purchase>
{
   Task<IEnumerable<Purchase>> GetPurchasesByUserId(int userId);
    Task<IEnumerable<TopPurchasedMovieModel>> GetTopPurchasedMovies(DateTime? fromDate, DateTime? toDate);
}
