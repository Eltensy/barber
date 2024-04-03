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
        private readonly DataContext _context;

        private bool _disposed = false;

        public ReviewRepository(DataContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetReviewByID(int reviewId)
        {
            return await _context.Reviews.FindAsync(reviewId);
        }

        public async Task InsertReview(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await Save();
        }

        public async Task DeleteReview(int reviewId)
        {
            Review? review = await _context.Reviews.FindAsync(reviewId);
            if (null != review) _context.Reviews.Remove(review);
            await Save();
        }

        public async Task UpdateReview(Review review)
        {
            _context.Entry(review).State = EntityState.Modified;
            await Save();
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
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
