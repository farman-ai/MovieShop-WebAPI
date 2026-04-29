using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IPurchaseRepository _purchaseRepository;
    public PurchaseService(IPurchaseRepository purchaseRepository) => _purchaseRepository = purchaseRepository;

    public async Task<List<PurchaseModel>> GetUserPurchases(int userId)
    {
        var purchases = await _purchaseRepository.GetPurchasesByUserId(userId);
        
        var modelList = new List<PurchaseModel>();
        foreach (var p in purchases)
        {
            modelList.Add(new PurchaseModel
            {
                UserId = p.UserId,
                MovieId = p.MovieId,
                PurchaseNumber = p.PurchaseNumber,
                TotalPrice = p.TotalPrice,
                PurchaseDateTime = p.PurchaseDateTime,
                // Yahan se aayegi Image aur Title
                Title = p.Movie?.Title ?? "Unknown",
                PosterUrl = p.Movie?.PosterUrl ?? "" 
            });
        }
        return modelList;
    }

    public async Task<bool> PurchaseMovie(PurchaseRequestModel request)
    {
        var purchase = new Purchase {
            UserId = request.UserId,
            MovieId = request.MovieId,
            TotalPrice = request.TotalPrice,
            PurchaseDateTime = request.PurchaseDateTime,
            PurchaseNumber = request.PurchaseNumber
        };
        var result = await _purchaseRepository.AddAsync(purchase);
        return result != null;
    }


    public async Task<bool> IsMoviePurchased(int userId, int movieId)
{
   
    var purchases = await _purchaseRepository.GetPurchasesByUserId(userId);
    
    
    return purchases.Any(p => p.MovieId == movieId);
}



public async Task<IEnumerable<TopPurchasedMovieModel>> GetTopMovies(DateTime? fromDate, DateTime? toDate)
{
    return await _purchaseRepository.GetTopPurchasedMovies(fromDate, toDate);
}
}