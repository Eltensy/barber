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
        public void DeleteAdmin(int adminId)
        {
            _adminRepository.DeleteAdmin(adminId);
            _adminRepository.Save();
        }

        public AdminDto GetAdminById(int adminId)
        {
            var admin = _adminRepository.GetAdminByID(adminId);
            var adminDto = new AdminDto()
            {
                Id = admin.Id,
                Name = admin.Name,
                Surname = admin.Surname,
                Phone = admin.Phone,
                Email = admin.Email,
                Password = admin.Password
            };
            return adminDto;
        }

        public List<AdminDto> GetAdmins()
        {
            var admins = _adminRepository.GetAdmins();
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

        public void InsertAdmin(AdminDto adminDto)
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
            _adminRepository.InsertAdmin(admin);
            _adminRepository.Save();
        }

        public void UpdateAdmin(AdminDto adminDto)
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

            _adminRepository.UpdateAdmin(admin);
            _adminRepository.Save();
        }
    }
}
