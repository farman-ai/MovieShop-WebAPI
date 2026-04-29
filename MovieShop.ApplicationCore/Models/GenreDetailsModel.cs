namespace MovieShop.ApplicationCore.Models;

public class GenreDetailsModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<MovieCardModel> Movies { get; set; } = new();
}