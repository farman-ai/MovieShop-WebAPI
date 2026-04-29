namespace MovieShop.ApplicationCore.Entities;

public class MovieCast
{
    public int MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int CastId { get; set; }
    public Cast Cast { get; set; } = null!;

    public string Character { get; set; } = "";
}