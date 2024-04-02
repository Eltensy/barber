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
    public class GuestRepository : IGuestRepository, IDisposable
    {
        private readonly DataContext _context;

        private bool _disposed = false;

        public GuestRepository(DataContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<Guest>> GetGuests()
        {
            return await _context.Guests.ToListAsync();
        }

        public async Task<Guest?> GetGuestByID(int guestId)
        {
            return await _context.Guests.FindAsync(guestId);
        }

        public async Task InsertGuest(Guest guest)
        {
            await _context.Guests.AddAsync(guest);
            await Save();
        }

        public async Task DeleteGuest(int guestId)
        {
            Guest? guest = await _context.Guests.FindAsync(guestId);
            if(null != guest) _context.Guests.Remove(guest);
            await Save();
        }

        public async Task UpdateGuest(Guest guest)
        {
            _context.Entry(guest).State = EntityState.Modified;
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
