using MovieShop.ApplicationCore.Entities;

namespace MovieShop.ApplicationCore.Contracts.Repository;

public interface IGenreRepository : IRepository<Genre>
{
    Task<IEnumerable<Genre>> GetAllGenres();
    Task<Genre?> GetGenreById(int id);
}