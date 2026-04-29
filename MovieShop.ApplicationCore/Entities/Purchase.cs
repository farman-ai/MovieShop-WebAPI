namespace MovieShop.ApplicationCore.Entities;

public class Purchase
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public decimal TotalPrice { get; set; }
    public DateTime PurchaseDateTime { get; set; }
    public Guid PurchaseNumber { get; set; }
}