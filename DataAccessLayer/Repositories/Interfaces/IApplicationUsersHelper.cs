using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IApplicationUsersHelper
    {
        public List<ApplicationUser> GetRolesToUsers(string roleName);
    }
}
