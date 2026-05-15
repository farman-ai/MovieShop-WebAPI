using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;
using MovieShop.WebAPI.Filters;

namespace MovieShop.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IAdminService _adminService;

    public AdminController(IPurchaseService purchaseService, IAdminService adminService)
    {
        _purchaseService = purchaseService;
        _adminService = adminService;
    }

    [HttpPost("movies")]
    [ServiceFilter(typeof(LogCreateMovieRequestFilter))]
    public async Task<IActionResult> CreateMovie([FromBody] MovieCreateRequestModel request)
    {
        var movie = await _adminService.CreateMovie(request);
        return CreatedAtAction(nameof(CreateMovie), new { id = movie.Id }, movie);
    }

    [HttpGet("top-movies")]
    public async Task<IActionResult> GetTopMovies(DateTime? fromDate, DateTime? toDate)
    {
        var movies = await _purchaseService.GetTopMovies(fromDate, toDate);
        return Ok(movies);
    }
}
