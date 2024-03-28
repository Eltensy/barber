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
    public class AdminRepository : IAdminRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public AdminRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Admin> GetAdmins()
        {
            return _context.Admins.ToList();
        }

        public Admin GetAdminByID(int adminId)
        {
            return _context.Admins.Find(adminId);
        }

        public void InsertAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
        }

        public void DeleteAdmin(int adminId)
        {
            Admin admin = _context.Admins.Find(adminId);
            _context.Admins.Remove(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
            _context.Entry(admin).State = EntityState.Modified;
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
