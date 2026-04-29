
using MovieShop.ApplicationCore.Models;
using MovieShop.ApplicationCore.Entities;

namespace MovieShop.ApplicationCore.Contracts.Services;

public interface IUserService
{
    Task RegisterUser(UserRegisterModel model);
    Task<User?> ValidateUser(string email, string password); 
Task AddMovieReview(Review review);
    Task UpdateMovieReview(Review review);
    Task<Review?> GetReviewDetails(int userId, int movieId);
}