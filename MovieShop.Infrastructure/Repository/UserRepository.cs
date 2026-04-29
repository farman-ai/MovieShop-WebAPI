// Path: MovieShop.Infrastructure/Repository/UserRepository.cs
using Microsoft.EntityFrameworkCore;
using MovieShop.ApplicationCore.Contracts.Repository;
using MovieShop.Infrastructure.Data;
using MovieShop.ApplicationCore.Entities;

namespace MovieShop.Infrastructure.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetUserByEmail(string email)
    {
     
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddReview(Review review)
{
    _dbContext.Reviews.Add(review);
    await _dbContext.SaveChangesAsync();
}

public async Task<Review?> GetReviewByUser(int userId, int movieId)
    {
        return await _dbContext.Reviews
            .FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
    }

    public async Task UpdateReview(Review review)
    {
        _dbContext.Reviews.Update(review);
        await _dbContext.SaveChangesAsync();
    }
}