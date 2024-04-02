using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public class AdminService : IAdminService
    {
        private readonly AdminRepository _adminRepository;

        public AdminService(AdminRepository adminRepository)
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
            if(null !=  admin)
            {
                adminDto = new AdminDto()
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Surname = admin.Surname,
                    Phone = admin.Phone,
                    Email = admin.Email,
                    Password = admin.Password
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
                                  Password = admin.Password
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
                Password = adminDto.Password
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
                Password = adminDto.Password
            };

            await _adminRepository.UpdateAdmin(admin);
        }
    }
}
