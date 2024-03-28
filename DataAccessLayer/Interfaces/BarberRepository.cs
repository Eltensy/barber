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
    public class BarberRepository : IBarberRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public BarberRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Barber> GetBarbers()
        {
            return _context.Barbers.ToList();
        }

        public Barber GetBarberByID(int barberId)
        {
            return _context.Barbers.Find(barberId);
        }

        public void InsertBarber(Barber barber)
        {
            _context.Barbers.Add(barber);
        }

        public void DeleteBarber(int barberId)
        {
            Barber barber = _context.Barbers.Find(barberId);
            _context.Barbers.Remove(barber);
        }

        public void UpdateBarber(Barber barber)
        {
            _context.Entry(barber).State = EntityState.Modified;
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
