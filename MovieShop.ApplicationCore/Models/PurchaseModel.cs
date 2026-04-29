namespace MovieShop.ApplicationCore.Models;

public class PurchaseModel
{
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public string Title { get; set; } = "";
    public string PosterUrl { get; set; } = "";
    public Guid PurchaseNumber { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDateTime { get; set; }
  
}