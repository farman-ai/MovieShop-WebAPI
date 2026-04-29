namespace MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;

public interface IMovieService
{
    Task<IEnumerable<MovieCardModel>> GetTopMovies();
    Task<IEnumerable<MovieCardModel>> GetHighestGrossingMovies();
    Task<MovieDetailsResponseModel?> GetMovieDetails(int id);
    Task<PagedResultSetModel<MovieCardModel>> GetMoviesByPagination(int pageNumber = 1, int pageSize = 18);
    Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenrePagination(int genreId, int pageSize = 18, int pageNumber = 1);
}