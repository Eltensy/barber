using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;

        public AdminRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin?> GetAdminByID(int adminId)
        {
            return await _context.Admins.FindAsync(adminId);
        }

        public async Task InsertAdmin(Admin admin)
        {
            await _context.Admins.AddAsync(admin);
            await Save();
        }

        public async Task DeleteAdmin(int adminId)
        {
            Admin? admin = await _context.Admins.FindAsync(adminId);
            if (null != admin) _context.Admins.Remove(admin);
            await Save();
        }

        public async Task UpdateAdmin(Admin admin)
        {
            _context.Entry(admin).State = EntityState.Modified;
            await Save();
        }

        public async Task<Admin?> GetAdminByEmail(string email)
        {
            Admin? admin = await _context.Admins.SingleOrDefaultAsync(x => x.Email.Equals(email));
            return admin;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
