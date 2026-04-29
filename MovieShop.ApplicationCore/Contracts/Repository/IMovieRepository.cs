namespace MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;

public interface IMovieRepository : IRepository<Movie>
{
    Task<IEnumerable<Movie>> GetHighestGrossingMovies();
    Task<Movie?> GetMovieById(int id);
    Task<PagedResultSetModel<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 18, int pageNumber = 1);
    Task<PagedResultSetModel<Movie>> GetMoviesByPagination(int pageNumber = 1, int pageSize = 18);
}