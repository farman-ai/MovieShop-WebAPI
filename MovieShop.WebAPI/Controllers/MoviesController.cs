using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly IPurchaseService _purchaseService;
    private readonly IGenreService _genreService;

    public MoviesController(IMovieService movieService, IPurchaseService purchaseService, IGenreService genreService)
    {
        _movieService = movieService;
        _purchaseService = purchaseService;
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResultSetModel<MovieCardModel>>> GetMovies(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 18)
    {
        var movies = await _movieService.GetMoviesByPagination(pageNumber, pageSize);
        return Ok(movies);
    }

    [HttpGet("top")]
    public async Task<ActionResult<IEnumerable<MovieCardModel>>> GetTopMovies()
    {
        var movies = await _movieService.GetTopMovies();
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetMovie(int id, [FromQuery] int? userId = null)
    {
        var movie = await _movieService.GetMovieDetails(id);
        if (movie == null)
        {
            return NotFound();
        }

        var isPurchased = userId is > 0 &&
                          await _purchaseService.IsMoviePurchased(userId.GetValueOrDefault(), id);

        return Ok(new { movie, isPurchased });
    }

    [HttpGet("genre/{genreId:int}")]
    public async Task<IActionResult> GetMoviesByGenre(
        int genreId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 18)
    {
        var genre = await _genreService.GetGenreById(genreId);
        if (genre == null)
        {
            return NotFound();
        }

        var movies = await _movieService.GetMoviesByGenrePagination(genreId, pageSize, pageNumber);
        return Ok(new { Genre = new { genre.Id, genre.Name }, Movies = movies });
    }

    [HttpPost("{id:int}/purchase")]
    public async Task<IActionResult> PurchaseMovie(int id, [FromBody] PurchaseRequestModel request)
    {
        if (request.UserId <= 0)
        {
            ModelState.AddModelError(nameof(request.UserId), "UserId is required.");
            return ValidationProblem(ModelState);
        }

        var movie = await _movieService.GetMovieDetails(id);
        if (movie == null)
        {
            return NotFound();
        }

        request.MovieId = id;
        request.TotalPrice = request.TotalPrice > 0 ? request.TotalPrice : movie.Price;
        request.PurchaseDateTime = request.PurchaseDateTime == default ? DateTime.UtcNow : request.PurchaseDateTime;
        request.PurchaseNumber = request.PurchaseNumber == default ? Guid.NewGuid() : request.PurchaseNumber;

        var purchased = await _purchaseService.PurchaseMovie(request);
        return purchased
            ? Created($"/api/users/{request.UserId}/purchases", request)
            : BadRequest(new { message = "Unable to purchase movie." });
    }
}
