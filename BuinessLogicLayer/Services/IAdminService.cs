using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IAdminService
    {
        Task<List<AdminDto>> GetAdmins();
        Task<AdminDto?> GetAdminById(int adminId);
        Task InsertAdmin(AdminDto adminDto);
        Task DeleteAdmin(int adminId);
        Task UpdateAdmin(AdminDto adminDto);
    }
}
