
namespace MovieShop.ApplicationCore.Models; 

public class TopPurchasedMovieModel
{
    public int MovieId { get; set; }
    public string Title { get; set; } = default!; 
    public int TotalPurchases { get; set; }
}