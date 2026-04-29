using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services;

public class GenreService : IGenreService
{
    private readonly IGenreRepository _genreRepository;

    public GenreService(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<GenreModel>> GetAllGenres()
    {
        var genres = await _genreRepository.GetAllGenres();

        return genres.Select(g => new GenreModel
        {
            Id = g.Id,
            Name = g.Name
        });
    }

    public async Task<GenreDetailsModel?> GetGenreById(int id)
{
    var genre = await _genreRepository.GetGenreById(id);

    if (genre == null) return null;

    return new GenreDetailsModel
    {
        Id = genre.Id,
        Name = genre.Name,
      
        Movies = genre.MovieGenres.Select(mg => new MovieCardModel
        {
            Id = mg.MovieId,
            Title = mg.Movie.Title,
            PosterUrl = mg.Movie.PosterUrl
        }).ToList()
    };
}
}
