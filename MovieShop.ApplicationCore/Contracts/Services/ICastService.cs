using MovieShop.ApplicationCore.Models;

namespace MovieShop.ApplicationCore.Contracts.Services;

public interface ICastService
{
    Task<CastDetailsResponseModel?> GetCastDetails(int id);
}