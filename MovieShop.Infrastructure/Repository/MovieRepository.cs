using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;
using MovieShop.Infrastructure.Data;

namespace MovieShop.Infrastructure.Repository;


public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Movie>> GetHighestGrossingMovies()
    {
        return await _dbContext.Movies
            .OrderByDescending(m => m.Revenue)
            .Take(30)
            .ToListAsync();
    }

    public async Task<Movie?> GetMovieById(int id)
    {
        return await _dbContext.Movies
            .Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
            .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
            .Include(m => m.Trailers)
            .Include(m => m.Reviews)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<PagedResultSetModel<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 18, int pageNumber = 1)
    {
        var totalMoviesCount = await _dbContext.MovieGenres
            .Where(mg => mg.GenreId == genreId)
            .CountAsync();

        var movies = await _dbContext.MovieGenres
            .Where(mg => mg.GenreId == genreId)
            .Include(mg => mg.Movie)
            .OrderByDescending(mg => mg.Movie.Revenue)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(mg => mg.Movie)
            .ToListAsync();

        return new PagedResultSetModel<Movie>(movies, pageNumber, pageSize, totalMoviesCount);
    }

    public async Task<PagedResultSetModel<Movie>> GetMoviesByPagination(int pageNumber = 1, int pageSize = 18)
    {
        var totalMoviesCount = await _dbContext.Movies.CountAsync();

        var movies = await _dbContext.Movies
            .OrderByDescending(m => m.Revenue)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResultSetModel<Movie>(movies, pageNumber, pageSize, totalMoviesCount);
    }
}