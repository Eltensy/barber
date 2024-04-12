using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task DeleteReview(int reviewId)
        {
            await _reviewRepository.DeleteReview(reviewId);
        }

        public async Task<ReviewDto?> GetReviewByID(int reviewId)
        {
            Review? review = await _reviewRepository.GetReviewByID(reviewId);
            ReviewDto? reviewDto = null;
            if (review != null)
            {
                reviewDto = new ReviewDto()
                {
                    Id = review.Id,
                    fk_ClientId = review.fk_ClientId,
                    fk_BarberId = review.fk_BarberId,
                    Text = review.Text,
                    Rating = review.Rating,
                    Date = review.Date
                };
            }
            return reviewDto;
        }

        public async Task<List<ReviewDto>> GetReviews()
        {
            var reviews = await _reviewRepository.GetReviews();
            var reviewsDtos = from review in reviews
                              select new ReviewDto()
                              {
                                  Id = review.Id,
                                  fk_ClientId = review.fk_ClientId,
                                  fk_BarberId = review.fk_BarberId,
                                  Text = review.Text,
                                  Rating = review.Rating,
                                  Date = review.Date
                              };
            return reviewsDtos.ToList();
        }

        public async Task<List<ReviewDto>> GetReviewsByBarberId(int fkBarberId)
        {
            var reviews = await _reviewRepository.GetReviewsByBarberId(fkBarberId);
            var reviewsDtos = from review in reviews
                              select new ReviewDto()
                              {
                                  Id = review.Id,
                                  fk_ClientId = review.fk_ClientId,
                                  fk_BarberId = review.fk_BarberId,
                                  Text = review.Text,
                                  Rating = review.Rating,
                                  Date = review.Date
                              };
            return reviewsDtos.ToList();
        }

        public async Task InsertReview(ReviewDto reviewDto)
        {
            Review review = new Review()
            {
                Id = reviewDto.Id,
                fk_ClientId = reviewDto.fk_ClientId,
                fk_BarberId = reviewDto.fk_BarberId,
                Text = reviewDto.Text,
                Rating = reviewDto.Rating,
                Date = reviewDto.Date
            };
            await _reviewRepository.InsertReview(review);
        }

        public async Task UpdateReview(ReviewDto reviewDto)
        {
            Review review = new Review()
            {
                Id = reviewDto.Id,
                fk_ClientId = reviewDto.fk_ClientId,
                fk_BarberId = reviewDto.fk_BarberId,
                Text = reviewDto.Text,
                Rating = reviewDto.Rating,
                Date = reviewDto.Date
            };
            await _reviewRepository.UpdateReview(review);
        }
    }
}
