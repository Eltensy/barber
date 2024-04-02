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
    public class VisitRepository : IVisitRepository, IDisposable
    {
        private readonly DataContext _context;

        private bool _disposed = false;

        public VisitRepository(DataContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<Visit>> GetVisits()
        {
            return await _context.Visits.ToListAsync();
        }

        public async Task<Visit?> GetVisitByID(int visitId)
        {
            return await _context.Visits.FindAsync(visitId);
        }

        public async Task InsertVisit(Visit visit)
        {
            await _context.Visits.AddAsync(visit);
            await Save();
        }

        public async Task DeleteVisit(int visitId)
        {
            Visit? visit = await _context.Visits.FindAsync(visitId);
            if(null != visit) _context.Visits.Remove(visit);
            await Save();
        }

        public async Task UpdateVisit(Visit visit)
        {
            _context.Entry(visit).State = EntityState.Modified;
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
