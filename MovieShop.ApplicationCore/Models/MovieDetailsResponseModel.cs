namespace MovieShop.ApplicationCore.Models;

public class MovieDetailsResponseModel
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Overview { get; set; } = "";
    public string Tagline { get; set; } = "";
    public int Runtime { get; set; }
    public decimal Budget { get; set; }
    public decimal Revenue { get; set; }
    public string PosterUrl { get; set; } = "";
    public string BackdropUrl { get; set; } = "";
    public string ImdbUrl { get; set; } = "";
    public string TmdbUrl { get; set; } = "";
    public string OriginalLanguage { get; set; } = "";
    public DateTime ReleaseDate { get; set; }
    public decimal Rating { get; set; }
    
    public decimal Price { get; set; }

    public List<GenreModel> Genres { get; set; } = new();
    public List<TrailerModel> Trailers { get; set; } = new();
    public List<CastModel> Casts { get; set; } = new();

    public List<MovieReviewResponseModel> Reviews { get; set; } = new();
}
public class MovieReviewResponseModel
{
    public int MovieId { get; set; }
    public int UserId { get; set; }
    public decimal Rating { get; set; }
    public string ReviewText { get; set; } = default!;
}

