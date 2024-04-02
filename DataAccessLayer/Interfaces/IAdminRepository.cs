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
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin?> GetAdminByID(int adminId);
        Task InsertAdmin(Admin admin);
        Task DeleteAdmin(int adminId);
        Task UpdateAdmin(Admin admin);
        Task Save();
    }
}
