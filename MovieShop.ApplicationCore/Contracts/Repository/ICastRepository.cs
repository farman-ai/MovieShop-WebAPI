using MovieShop.ApplicationCore.Entities;

namespace MovieShop.ApplicationCore.Contracts.Repository;

public interface ICastRepository : IRepository<Cast>
{
    Task<Cast?> GetCastById(int id);
}