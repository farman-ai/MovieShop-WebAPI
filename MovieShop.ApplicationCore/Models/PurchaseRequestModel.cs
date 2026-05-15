namespace MovieShop.ApplicationCore.Models;

using MovieShop.ApplicationCore.Validators;

public class PurchaseRequestModel
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }

    [NotPastPurchaseDate]
    public DateTime PurchaseDateTime { get; set; } 
    public Guid PurchaseNumber { get; set; }
}
