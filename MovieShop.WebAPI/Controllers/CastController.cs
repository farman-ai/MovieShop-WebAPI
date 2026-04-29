using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;

namespace MovieShop.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CastController : ControllerBase
{
    private readonly ICastService _castService;

    public CastController(ICastService castService)
    {
        _castService = castService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCast(int id)
    {
        var cast = await _castService.GetCastDetails(id);
        return cast == null ? NotFound() : Ok(cast);
    }
}
