using System.Security.Cryptography;
using System.Text;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.ApplicationCore.Contracts.Services;
using MovieShop.ApplicationCore.Models;
using MovieShop.ApplicationCore.Entities;

namespace MovieShop.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterUser(UserRegisterModel model)
    {
        // 1. Check if user already exists
        var existingUser = await _userRepository.GetUserByEmail(model.Email);
        if (existingUser != null)
        {
            throw new Exception("User already exists with this email.");
        }

        // 2. Map data and HASH the password
        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            HashedPassword = HashPassword(model.Password) // <-- Hashing here
        };

        await _userRepository.Add(user);
    }

    public async Task<User?> ValidateUser(string email, string password)
    {
        // 1. Get user by email
        var user = await _userRepository.GetUserByEmail(email);
        if (user == null) return null;

        // 2. Hash the incoming password and compare with DB hash
        var hashedInputPassword = HashPassword(password);

        if (user.HashedPassword == hashedInputPassword)
        {
            return user; // Password match
        }

        return null; // Password mismatch
    }


    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Convert password string to byte array
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            // Convert hashed bytes back to string (Base64)
            return Convert.ToBase64String(hashedBytes);
        }
    }

public async Task AddMovieReview(Review review)
{
    await _userRepository.AddReview(review);
}

public async Task UpdateMovieReview(Review review)
{
    await _userRepository.UpdateReview(review);
}

public async Task<Review?> GetReviewDetails(int userId, int movieId)
{
    return await _userRepository.GetReviewByUser(userId, movieId);
}

}