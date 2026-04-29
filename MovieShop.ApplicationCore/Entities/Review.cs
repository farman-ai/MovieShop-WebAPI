namespace MovieShop.ApplicationCore.Entities;

public class Review
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public decimal Rating { get; set; }
    public string ReviewText { get; set; } = "";
    public DateTime CreatedDate { get; set; }
}