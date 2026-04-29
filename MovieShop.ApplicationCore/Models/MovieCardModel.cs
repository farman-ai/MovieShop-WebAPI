namespace MovieShop.ApplicationCore.Models;

public class MovieCardModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public List<GenreModel> Genres { get; set; } = new();
}