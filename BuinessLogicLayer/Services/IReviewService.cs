using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetReviews();
        Task<List<ReviewDto>> GetReviewsByBarberId(int fkBarberId);
        Task<ReviewDto?> GetReviewByID(int reviewId);
        Task InsertReview(ReviewDto reviewDto);
        Task DeleteReview(int reviewId);
        Task UpdateReview(ReviewDto reviewDto);
    }
}
