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
    public class GuestRepository : IGuestRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public GuestRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Guest> GetGuests()
        {
            return _context.Guests.ToList();
        }

        public Guest GetGuestByID(int guestId)
        {
            return _context.Guests.Find(guestId);
        }

        public void InsertGuest(Guest guest)
        {
            _context.Guests.Add(guest);
        }

        public void DeleteGuest(int guestId)
        {
            Guest guest = _context.Guests.Find(guestId);
            _context.Guests.Remove(guest);
        }

        public void UpdateGuest(Guest guest)
        {
            _context.Entry(guest).State = EntityState.Modified;
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
