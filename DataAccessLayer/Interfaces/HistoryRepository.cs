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
        private readonly DataContext _context;

        private bool _disposed = false;

        public HistoryRepository(DataContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<History>> GetHistorys()
        {
            return await _context.History.ToListAsync();
        }

        public async Task<History?> GetHistoryByID(int historyId)
        {
            return await _context.History.FindAsync(historyId);
        }

        public async Task InsertHistory(History history)
        {
            await _context.History.AddAsync(history);
            await Save();
        }

        public async Task DeleteHistory(int historyId)
        {
            History? history = await _context.History.FindAsync(historyId);
            if(null != history) _context.History.Remove(history);
            await Save();
        }

        public async Task UpdateHistory(History history)
        {
            _context.Entry(history).State = EntityState.Modified;
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
