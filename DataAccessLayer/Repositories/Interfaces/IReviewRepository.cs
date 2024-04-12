using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviews();
        Task<Review?> GetReviewByID(int reviewId);
        Task<IEnumerable<Review>> GetReviewsByBarberId(int fkBarberId);
        Task InsertReview(Review review);
        Task DeleteReview(int reviewId);
        Task UpdateReview(Review review);
        Task Save();
    }
}
