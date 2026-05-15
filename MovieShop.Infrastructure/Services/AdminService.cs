using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly IMovieRepository _movieRepository;

    public AdminService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Movie> CreateMovie(MovieCreateRequestModel request)
    {
        var movie = new Movie
        {
            Title = request.Title,
            Overview = request.Overview,
            Tagline = request.Tagline,
            Runtime = request.Runtime,
            Budget = request.Budget,
            Revenue = request.Revenue,
            PosterUrl = request.PosterUrl,
            BackdropUrl = request.BackdropUrl,
            ImdbUrl = request.ImdbUrl,
            TmdbUrl = request.TmdbUrl,
            StreamUrl = request.StreamUrl,
            OriginalLanguage = request.OriginalLanguage,
            ReleaseDate = request.ReleaseDate
        };

        return await _movieRepository.AddAsync(movie);
    }
}
