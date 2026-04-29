using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;

namespace MovieShop.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public AdminController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpGet("top-movies")]
    public async Task<IActionResult> GetTopMovies(DateTime? fromDate, DateTime? toDate)
    {
        var movies = await _purchaseService.GetTopMovies(fromDate, toDate);
        return Ok(movies);
    }
}
