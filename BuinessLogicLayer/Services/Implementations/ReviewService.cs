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

        public async Task<List<ReviewDto>> GetReviews()
        {
            var reviews = await _reviewRepository.GetReviews();
            var reviewsDtos = from review in reviews
                              select new ReviewDto(review);
                              //{
                              //    Id = review.Id,
                              //    fk_ClientId = review.fk_ClientId,
                              //    fk_BarberId = review.fk_BarberId,
                              //    Text = review.Text,
                              //    Rating = review.Rating,
                              //    Date = review.Date
                              //};
            return reviewsDtos.ToList();
        }

        public async Task<ReviewDto?> GetReviewByID(int reviewId)
        {
            Review? review = await _reviewRepository.GetReviewByID(reviewId);
            ReviewDto? reviewDto = null;
            if (review != null)
            {
                reviewDto = new ReviewDto(review);
                //{
                //    Id = review.Id,
                //    fk_ClientId = review.fk_ClientId,
                //    fk_BarberId = review.fk_BarberId,
                //    Text = review.Text,
                //    Rating = review.Rating,
                //    Date = review.Date
                //};
            }
            return reviewDto;
        }


        public async Task<List<ReviewDto>> GetReviewsByBarberId(string fkBarberId)
        {
            var reviews = await _reviewRepository.GetReviewsByBarberId(fkBarberId);
            var reviewsDtos = from review in reviews
                              select new ReviewDto(review);
                              //{
                              //    Id = review.Id,
                              //    fk_ClientId = review.fk_ClientId,
                              //    fk_BarberId = review.fk_BarberId,
                              //    Text = review.Text,
                              //    Rating = review.Rating,
                              //    Date = review.Date
                              //};
            return reviewsDtos.ToList();
        }

        public async Task<List<ReviewDto>> GetReviewsByClientId(string fkClientId)
        {
            var reviews = await _reviewRepository.GetReviewsByClientId(fkClientId);
            var reviewsDtos = from review in reviews
                              select new ReviewDto(review);

            return reviewsDtos.ToList();
        }

        public async Task InsertReview(ReviewDto reviewDto)
        {
            //Review review = new Review()
            //{
            //    Id = reviewDto.Id,
            //    fk_ClientId = reviewDto.fk_ClientId,
            //    fk_BarberId = reviewDto.fk_BarberId,
            //    Text = reviewDto.Text,
            //    Rating = reviewDto.Rating,
            //    Date = reviewDto.Date
            //};

            Review review = reviewDto.ToEntity();
            await _reviewRepository.InsertReview(review);
        }

        public async Task UpdateReview(ReviewDto reviewDto)
        {
            //Review review = new Review()
            //{
            //    Id = reviewDto.Id,
            //    fk_ClientId = reviewDto.fk_ClientId,
            //    fk_BarberId = reviewDto.fk_BarberId,
            //    Text = reviewDto.Text,
            //    Rating = reviewDto.Rating,
            //    Date = reviewDto.Date
            //};

            Review review = reviewDto.ToEntity();
            await _reviewRepository.UpdateReview(review);
        }
        public async Task DeleteReview(int reviewId)
        {
            await _reviewRepository.DeleteReview(reviewId);
        }
    }
}
