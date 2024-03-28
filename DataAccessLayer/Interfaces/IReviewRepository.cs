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
        IEnumerable<Review> GetReviews();
        Review GetReviewByID(int reviewId);
        void InsertReview(Review review);
        void DeleteReview(int reviewId);
        void UpdateReview(Review review);
        void Save();
    }
}
