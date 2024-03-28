using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IAdminRepository : IDisposable
    {
        IEnumerable<Admin> GetAdmins();
        Admin GetAdminByID(int adminId);
        void InsertAdmin(Admin admin);
        void DeleteAdmin(int adminId);
        void UpdateAdmin(Admin admin);
        void Save();
    }
}
