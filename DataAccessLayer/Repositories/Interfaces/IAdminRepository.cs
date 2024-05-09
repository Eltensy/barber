using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin?> GetAdminByID(string adminId);
        // Task InsertAdmin(Admin admin);
        // Task DeleteAdmin(string adminId);
        // Task UpdateAdmin(Admin admin);
        Task<Admin?> GetAdminByEmail(string email);
        // Task Save();
    }
}
