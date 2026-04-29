using MovieShop.ApplicationCore.Models;

namespace MovieShop.ApplicationCore.Contracts.Services;

public interface IGenreService
{
    Task<IEnumerable<GenreModel>> GetAllGenres();
    Task<GenreDetailsModel?> GetGenreById(int id);
}