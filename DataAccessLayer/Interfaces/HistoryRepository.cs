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
    public class HistoryRepository : IHistoryRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public HistoryRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<History> GetHistorys()
        {
            return _context.History.ToList();
        }

        public History GetHistoryByID(int historyId)
        {
            return _context.History.Find(historyId);
        }

        public void InsertHistory(History history)
        {
            _context.History.Add(history);
        }

        public void DeleteHistory(int historyId)
        {
            History history = _context.History.Find(historyId);
            _context.History.Remove(history);
        }

        public void UpdateHistory(History history)
        {
            _context.Entry(history).State = EntityState.Modified;
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
