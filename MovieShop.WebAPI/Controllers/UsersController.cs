using Microsoft.AspNetCore.Mvc;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Entities;
using MovieShop.ApplicationCore.Models;

namespace MovieShop.WebAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;
    private readonly IUserService _userService;

    public UsersController(IPurchaseService purchaseService, IUserService userService)
    {
        _purchaseService = purchaseService;
        _userService = userService;
    }

    [HttpGet("{userId:int}/purchases")]
    public async Task<IActionResult> GetPurchases(int userId)
    {
        if (userId <= 0)
        {
            return BadRequest(new { message = "A valid userId is required." });
        }

        var purchases = await _purchaseService.GetUserPurchases(userId);
        return Ok(purchases);
    }

    [HttpPost("{userId:int}/purchases")]
    public async Task<IActionResult> BuyMovie(int userId, [FromBody] PurchaseRequestModel request)
    {
        if (userId <= 0)
        {
            return BadRequest(new { message = "A valid userId is required." });
        }

        request.UserId = userId;
        request.PurchaseDateTime = request.PurchaseDateTime == default ? DateTime.UtcNow : request.PurchaseDateTime;
        request.PurchaseNumber = request.PurchaseNumber == default ? Guid.NewGuid() : request.PurchaseNumber;

        var purchased = await _purchaseService.PurchaseMovie(request);
        return purchased
            ? Created($"/api/users/{userId}/purchases", request)
            : BadRequest(new { message = "Unable to purchase movie." });
    }

    [HttpPost("{userId:int}/reviews")]
    public async Task<IActionResult> SubmitReview(int userId, [FromBody] ReviewRequestModel request)
    {
        if (userId <= 0)
        {
            return BadRequest(new { message = "A valid userId is required." });
        }

        var rating = request.Rating >= 10 ? 9.9m : request.Rating;
        var existingReview = await _userService.GetReviewDetails(userId, request.MovieId);
        if (existingReview != null)
        {
            existingReview.Rating = rating;
            existingReview.ReviewText = request.ReviewText;
            existingReview.CreatedDate = DateTime.UtcNow;
            await _userService.UpdateMovieReview(existingReview);
        }
        else
        {
            await _userService.AddMovieReview(new Review
            {
                MovieId = request.MovieId,
                UserId = userId,
                Rating = rating,
                ReviewText = request.ReviewText,
                CreatedDate = DateTime.UtcNow
            });
        }

        return Ok(new { message = "Review submitted successfully." });
    }
}
