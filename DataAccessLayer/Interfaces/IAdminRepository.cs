using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin?> GetAdminByID(int adminId);
        Task InsertAdmin(Admin admin);
        Task DeleteAdmin(int adminId);
        Task UpdateAdmin(Admin admin);
        Task<Admin?> GetAdminByEmail(string email);
        Task Save();
    }
}
