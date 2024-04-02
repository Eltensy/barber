using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IReviewRepository : IDisposable
    {
        Task<IEnumerable<Review>> GetReviews();
        Task<Review?> GetReviewByID(int reviewId);
        Task InsertReview(Review review);
        Task DeleteReview(int reviewId);
        Task UpdateReview(Review review);
        Task Save();
    }
}
