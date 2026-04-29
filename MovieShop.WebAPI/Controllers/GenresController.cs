using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;

namespace MovieShop.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenresController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGenres()
    {
        var genres = await _genreService.GetAllGenres();
        return Ok(genres);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _genreService.GetGenreById(id);
        return genre == null ? NotFound() : Ok(genre);
    }
}
