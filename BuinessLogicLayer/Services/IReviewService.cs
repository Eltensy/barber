using BuinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public interface IReviewService
    {
        Task<List<ReviewDto>> GetReviews();
        Task<ReviewDto?> GetReviewByID(int reviewId);
        Task InsertReview(ReviewDto reviewDto);
        Task DeleteReview(int reviewId);
        Task UpdateReview(ReviewDto reviewDto);
    }
}
