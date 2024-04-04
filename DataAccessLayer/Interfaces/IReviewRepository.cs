using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviews();
        Task<Review?> GetReviewByID(int reviewId);
        Task InsertReview(Review review);
        Task DeleteReview(int reviewId);
        Task UpdateReview(Review review);
        Task Save();
    }
}
