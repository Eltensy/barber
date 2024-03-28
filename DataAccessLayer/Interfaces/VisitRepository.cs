using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class VisitRepository : IVisitRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public VisitRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Visit> GetVisits()
        {
            return _context.Visits.ToList();
        }

        public Visit GetVisitByID(int visitId)
        {
            return _context.Visits.Find(visitId);
        }

        public void InsertVisit(Visit visit)
        {
            _context.Visits.Add(visit);
        }

        public void DeleteVisit(int visitId)
        {
            Visit visit = _context.Visits.Find(visitId);
            _context.Visits.Remove(visit);
        }

        public void UpdateVisit(Visit visit)
        {
            _context.Entry(visit).State = EntityState.Modified;
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
