using BuinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public interface IAdminService
    {
        List<AdminDto> GetAdmins();
        AdminDto GetAdminById(int adminId);
        void InsertAdmin(AdminDto adminDto);
        void DeleteAdmin(int adminId);
        void UpdateAdmin(AdminDto adminDto);
    }
}
