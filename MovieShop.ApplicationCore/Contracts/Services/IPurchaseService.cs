using MovieShop.ApplicationCore.Models;

namespace MovieShop.ApplicationCore.Contracts.Services;

public interface IPurchaseService
{
    Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest);
    Task<List<PurchaseModel>> GetUserPurchases(int userId);
    Task<bool> IsMoviePurchased(int userId, int movieId);
    Task<IEnumerable<TopPurchasedMovieModel>> GetTopMovies(DateTime? fromDate, DateTime? toDate);
}