using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;

    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }

    public async Task<CastDetailsResponseModel?> GetCastDetails(int id)
    {
        var cast = await _castRepository.GetCastById(id);
        if (cast == null) return null;

        return new CastDetailsResponseModel
        {
            Id = cast.Id,
            Name = cast.Name,
            Gender = cast.Gender,
            TmdbUrl = cast.TmdbUrl,
            ProfilePath = cast.ProfilePath,
            Movies = cast.MovieCasts.Select(mc => new MovieCardModel
            {
                Id = mc.MovieId,
                Title = mc.Movie.Title,
                PosterUrl = mc.Movie.PosterUrl
            }).ToList()
        };
    }
}