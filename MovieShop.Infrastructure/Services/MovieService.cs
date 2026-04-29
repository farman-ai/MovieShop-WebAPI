using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
   
    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }


    public async Task<IEnumerable<MovieCardModel>> GetTopMovies()
    {
       return await Task.FromResult(new List<MovieCardModel>
        {
            new MovieCardModel { Id = 1, Title = "Domestic Disturbance", PosterUrl = "https://image.tmdb.org/t/p/w342/jdGniF19AqQ1taLXmK04b6BjQk.jpg" },
            new MovieCardModel { Id = 2, Title = "The Hard Way", PosterUrl = "https://image.tmdb.org/t/p/w342/1Nu42swt2QbHGV5o69UJL9EaprJ.jpg" },
            new MovieCardModel { Id = 3, Title = "Death to Smoochy", PosterUrl = "https://image.tmdb.org/t/p/w342/jLOgkwcXzlCmwjbWVaWS1vETutl.jpg" },
            new MovieCardModel { Id = 4, Title = "Sweet Smell of Success", PosterUrl = "https://image.tmdb.org/t/p/w342/7xfLUISDZ1zZ8vPU5Y7j5mv0Xvc.jpg" }
        });
    }

    // Highest Grossing Movies logic
    public async Task<IEnumerable<MovieCardModel>> GetHighestGrossingMovies()
    {
        var movies = await _movieRepository.GetHighestGrossingMovies();
        return movies.Select(m => new MovieCardModel
        {
            Id = m.Id,
            Title = m.Title,
            PosterUrl = m.PosterUrl
        });
    }

    // Movie Details Logic
    public async Task<MovieDetailsResponseModel?> GetMovieDetails(int id)
    {
        var movie = await _movieRepository.GetMovieById(id);
        if (movie == null) return null;
    
        var random = new Random();
        decimal generatedPrice = (decimal)random.Next(5, 20) + 0.99m;

        return new MovieDetailsResponseModel
        {
            Id = movie.Id,
            Title = movie.Title,
            Overview = movie.Overview,
            Tagline = movie.Tagline,
            Runtime = movie.Runtime,
            Budget = movie.Budget,
            Revenue = movie.Revenue,
            PosterUrl = movie.PosterUrl,
            BackdropUrl = movie.BackdropUrl,
            ImdbUrl = movie.ImdbUrl,
            TmdbUrl = movie.TmdbUrl,
            OriginalLanguage = movie.OriginalLanguage,
            ReleaseDate = movie.ReleaseDate,
            Price = generatedPrice,
            Rating = movie.Reviews.Any() ? movie.Reviews.Average(r => r.Rating) : 0,
            Reviews = movie.Reviews.Select(r => new MovieReviewResponseModel
        {
            MovieId = r.MovieId,
            UserId = r.UserId,
            Rating = r.Rating,
            ReviewText = r.ReviewText
        }).ToList(),
            Genres = movie.MovieGenres.Select(mg => new GenreModel { Id = mg.GenreId, Name = mg.Genre.Name }).ToList(),
            Trailers = movie.Trailers.Select(t => new TrailerModel { Id = t.Id, Name = t.Name, TrailerUrl = t.TrailerUrl }).ToList(),
            Casts = movie.MovieCasts.Select(mc => new CastModel { Id = mc.CastId, Name = mc.Cast.Name, Character = mc.Character, ProfilePath = mc.Cast.ProfilePath }).ToList(),
        };
    }

  
    public async Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 18, int pageNumber = 1)
    {

        var pagedMovies = await _movieRepository.GetMoviesByGenrePagination(genreId, pageSize, pageNumber);

        var movieCards = pagedMovies.Data.Select(m => new MovieCardModel
        {
            Id = m.Id,
            Title = m.Title,
            PosterUrl = m.PosterUrl,
            Genres = m.MovieGenres?.Select(mg => new GenreModel { Id = mg.GenreId, Name = mg.Genre.Name }).ToList() ?? new List<GenreModel>()
        }).ToList();

        return new PagedResultSetModel<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.TotalCount);
    }

    // All Movies Pagination
    public async Task<PagedResultSetModel<MovieCardModel>> GetMoviesByPagination(int pageNumber = 1, int pageSize = 18)
    {
        var pagedMovies = await _movieRepository.GetMoviesByPagination(pageNumber, pageSize);

        var movieCards = pagedMovies.Data.Select(m => new MovieCardModel
        {
            Id = m.Id,
            Title = m.Title,
            PosterUrl = m.PosterUrl
        }).ToList();

        return new PagedResultSetModel<MovieCardModel>(movieCards, pageNumber, pageSize, pagedMovies.TotalCount);
    }
}