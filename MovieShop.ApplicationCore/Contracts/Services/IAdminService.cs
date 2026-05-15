namespace MovieShop.ApplicationCore.Contracts.Services;

using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;

public interface IAdminService
{
    Task<Movie> CreateMovie(MovieCreateRequestModel request);
}
