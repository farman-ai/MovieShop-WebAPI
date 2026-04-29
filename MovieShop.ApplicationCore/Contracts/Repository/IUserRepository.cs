using MovieShop.ApplicationCore.Entities;

namespace MovieShop.ApplicationCore.Contracts.Repository;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string email);
    Task AddReview(Review review);
    Task UpdateReview(Review review);
    Task<Review?> GetReviewByUser(int userId, int movieId);

}