using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations
{
    public class BarberRepository : IBarberRepository
    {
        private readonly DataContext _context;
        private readonly IApplicationUsersHelper _applicationUsersHelper;

        public BarberRepository(DataContext context, IApplicationUsersHelper applicationUsersHelper)
        {
            _context = context;
            _applicationUsersHelper = applicationUsersHelper;
        }

        public async Task<IEnumerable<Barber>> GetBarbers()
        {
            //return await _context.Barbers.ToListAsync();

            List<Barber> barbers = new List<Barber>();

            var barberRoles = await _applicationUsersHelper.GetRolesToUsers("Barber");
            foreach (var barberRole in barberRoles)
            {
                barbers.Add(new Barber(barberRole));
            }

            return barbers.ToList();


        }

        public async Task<Barber?> GetBarberByID(string barberId)
        {
            //return await _context.Barbers.FindAsync(barberId);

            var appUser = await _context.ApplicationUsers.FindAsync(barberId);
            if (appUser == null)
            {
                return null;
            }
            return new Barber(appUser);
        }

        //public async Task InsertBarber(Barber barber)
        //{
        //    await _context.Barbers.AddAsync(barber);
        //    await Save();
        //}

        //public async Task DeleteBarber(int barberId)
        //{
        //    Barber? barber = await _context.Barbers.FindAsync(barberId);
        //    if (null != barber) _context.Barbers.Remove(barber);
        //    await Save();
        //}

        //public async Task UpdateBarber(Barber barber)
        //{
        //    _context.Entry(barber).State = EntityState.Modified;
        //    await Save();
        //}

        public async Task<Barber?> GetBarberByEmail(string email)
        {
            //Barber? barber = await _context.Barbers.SingleOrDefaultAsync(x => x.Email.Equals(email));
            //return barber;

            //ApplicationUser? appUser = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Email.Equals(email));
            var appUsers = await _applicationUsersHelper.GetRolesToUsers("Barber");
            ApplicationUser? appUser = appUsers.Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (appUser == null)
            {
                return null;
            }

            return new Barber(appUser);
        }

        //public async Task Save()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}
