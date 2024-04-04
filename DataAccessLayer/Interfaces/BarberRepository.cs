using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class BarberRepository : IBarberRepository
    {
        private readonly DataContext _context;

        public BarberRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Barber>> GetBarbers()
        {
            return await _context.Barbers.ToListAsync();
        }

        public async Task<Barber?> GetBarberByID(int barberId)
        {
            return await _context.Barbers.FindAsync(barberId);
        }

        public async Task InsertBarber(Barber barber)
        {
            await _context.Barbers.AddAsync(barber);
            await Save();
        }

        public async Task DeleteBarber(int barberId)
        {
            Barber? barber = await _context.Barbers.FindAsync(barberId);
            if (null != barber) _context.Barbers.Remove(barber);
            await Save();
        }

        public async Task UpdateBarber(Barber barber)
        {
            _context.Entry(barber).State = EntityState.Modified;
            await Save();
        }

        public async Task<Barber?> GetBarberByEmail(string email)
        {
            Barber? barber = await _context.Barbers.SingleOrDefaultAsync(x => x.Email.Equals(email));
            return barber;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
