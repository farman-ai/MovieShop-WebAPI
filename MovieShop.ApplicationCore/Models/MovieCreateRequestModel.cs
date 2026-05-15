using System.ComponentModel.DataAnnotations;

namespace MovieShop.ApplicationCore.Models;

public class MovieCreateRequestModel
{
    [Required]
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
    public string StreamUrl { get; set; } = "";
    public string OriginalLanguage { get; set; } = "";
    public DateTime ReleaseDate { get; set; }
}
