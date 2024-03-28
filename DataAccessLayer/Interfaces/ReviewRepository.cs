using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class ReviewRepository : IReviewRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public ReviewRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public Review GetReviewByID(int reviewId)
        {
            return _context.Reviews.Find(reviewId);
        }

        public void InsertReview(Review review)
        {
            _context.Reviews.Add(review);
        }

        public void DeleteReview(int reviewId)
        {
            Review review = _context.Reviews.Find(reviewId);
            _context.Reviews.Remove(review);
        }

        public void UpdateReview(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
