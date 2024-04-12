using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IAdminService
    {
        Task<List<AdminDto>> GetAdmins();
        Task<AdminDto?> GetAdminById(int adminId);
        Task InsertAdmin(AdminDto adminDto);
        Task DeleteAdmin(int adminId);
        Task UpdateAdmin(AdminDto adminDto);
        Task<AdminDto?> GetAdminByEmail(string email);
    }
}
