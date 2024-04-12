using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public async Task DeleteAdmin(int adminId)
        {
            await _adminRepository.DeleteAdmin(adminId);
        }

        public async Task<AdminDto?> GetAdminById(int adminId)
        {
            var admin = await _adminRepository.GetAdminByID(adminId);

            AdminDto? adminDto = null;
            if (null != admin)
            {
                adminDto = new AdminDto()
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Surname = admin.Surname,
                    Phone = admin.Phone,
                    Email = admin.Email,
                    PasswordHash = admin.PasswordHash
                };
            }
            return adminDto;
        }

        public async Task<List<AdminDto>> GetAdmins()
        {
            var admins = await _adminRepository.GetAdmins();
            var adminsDtos = from admin in admins
                             select new AdminDto()
                             {
                                 Id = admin.Id,
                                 Name = admin.Name,
                                 Surname = admin.Surname,
                                 Phone = admin.Phone,
                                 Email = admin.Email,
                                 PasswordHash = admin.PasswordHash
                             };
            return adminsDtos.ToList();
        }

        public async Task InsertAdmin(AdminDto adminDto)
        {
            Admin admin = new Admin()
            {
                Id = adminDto.Id,
                Name = adminDto.Name,
                Surname = adminDto.Surname,
                Phone = adminDto.Phone,
                Email = adminDto.Email,
                PasswordHash = adminDto.PasswordHash
            };
            await _adminRepository.InsertAdmin(admin);
        }

        public async Task UpdateAdmin(AdminDto adminDto)
        {
            Admin admin = new Admin()
            {
                Id = adminDto.Id,
                Name = adminDto.Name,
                Surname = adminDto.Surname,
                Phone = adminDto.Phone,
                Email = adminDto.Email,
                PasswordHash = adminDto.PasswordHash
            };

            await _adminRepository.UpdateAdmin(admin);
        }

        public async Task<AdminDto?> GetAdminByEmail(string email)
        {
            Admin? admin = await _adminRepository.GetAdminByEmail(email);
            AdminDto? adminDto = null;
            if (admin != null)
            {
                adminDto = new AdminDto()
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Surname = admin.Surname,
                    Email = admin.Email,
                    Phone = admin.Phone,
                    PasswordHash = admin.PasswordHash,
                };
            }

            return adminDto;
        }
    }
}
