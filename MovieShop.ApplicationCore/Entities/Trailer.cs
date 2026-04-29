namespace MovieShop.ApplicationCore.Entities;

public class Trailer
{
    public int Id { get; set; }

    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public string TrailerUrl { get; set; } = "";
    public string Name { get; set; } = "";
}