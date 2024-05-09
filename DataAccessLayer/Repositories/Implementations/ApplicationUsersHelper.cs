using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessLayer.Repositories.Implementations
{
    public class ApplicationUsersHelper : IApplicationUsersHelper
    {
        private readonly DataContext _context;

        public ApplicationUsersHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetRolesToUsers(string roleName)
        {
            //var roleManager =
            //    // new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //    new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context), null, null, null, null);
            //var role = await roleManager.FindByNameAsync(roleName);
            //var usersInRole =
            // _context.ApplicationUsers.Where(u => u.Roles.Select(r => r.RoleId).Contains(role.RoleId)).ToList();
            //return usersInRole;

            // Id of the selected role


            //var roleId = _context.Roles.Where(x => x.Name == roleName).First().Id; 
            var usersInRole = new List<ApplicationUser>();

            //var roleToFind = await _context.Roles.FindAsync(role);
            //if(roleToFind == null) return usersInRole;
            var roleToFind = await _context.Roles.Where(x => x.Name == roleName).ToListAsync();
            string roleId = roleToFind.FirstOrDefault().Id;
            // UserRoles rows with selected role
            var usersRole = _context.UserRoles.Where(role => role.RoleId == roleId).ToList(); 

            foreach (var user in usersRole)
            {
                var userInRole = await _context.ApplicationUsers.Where(x => x.Id == user.UserId).FirstAsync();
                usersInRole.Add(userInRole);
            }

            //var users = _context.ApplicationUsers
            //    .Join(_context.UserRoles.Where(role => role.RoleId == roleId),
            //    x => x.Id,
            //    y => y.UserId,
            //    (x, y) => new { }
            //    );

            //foreach (var user in )

            // var joined = _context.UserRoles.Join(_context.ApplicationUsers, x => x.UserId, y => y.Id, );

            // var users =
            //    _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(ddlRole)).ToList();

            return usersInRole;
        }
    }
}
