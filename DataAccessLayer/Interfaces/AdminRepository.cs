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
    public class AdminRepository : IAdminRepository, IDisposable
    {
        private readonly DataContext _context;

        private bool _disposed = false;

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
            if(null != admin) _context.Admins.Remove(admin);
            await Save();
        }

        public async Task UpdateAdmin(Admin admin)
        {
            _context.Entry(admin).State = EntityState.Modified;
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
