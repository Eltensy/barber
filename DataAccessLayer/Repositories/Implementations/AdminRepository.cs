using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        private IApplicationUsersHelper _applicationUsersHelper;

        public AdminRepository(DataContext context, IApplicationUsersHelper applicationUsersHelper)
        {
            _context = context;
            _applicationUsersHelper = applicationUsersHelper;
        }

        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            List<Admin> admins = new List<Admin>();

            var adminRoles = await _applicationUsersHelper.GetRolesToUsers("Admin");
            foreach (var adminRole in adminRoles)
            {
                admins.Add(new Admin(adminRole));
            }

            return admins.ToList();
        }

        public async Task<Admin?> GetAdminByID(string adminId)
        {
            var appUser = await _context.ApplicationUsers.FindAsync(adminId);
            if (appUser == null)
            {
                return null;
            }
            return new Admin(appUser);
        }

        //public async Task InsertAdmin(Admin admin)
        //{
        //    await _context.Admins.AddAsync(admin);
        //    await Save();
        //}

        //public async Task DeleteAdmin(int adminId)
        //{
        //    Admin? admin = await _context.Admins.FindAsync(adminId);
        //    if (null != admin) _context.Admins.Remove(admin);
        //    await Save();
        //}

        //public async Task UpdateAdmin(Admin admin)
        //{
        //    _context.Entry(admin).State = EntityState.Modified;
        //    await Save();
        //}

        public async Task<Admin?> GetAdminByEmail(string email)
        {
            //ApplicationUser? appUser = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Email.Equals(email));
            var appUsers = await _applicationUsersHelper.GetRolesToUsers("Admin");
            ApplicationUser? appUser = appUsers.Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (appUser == null)
            {
                return null;
            }

            return new Admin(appUser);
        }

        //public async Task Save()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}
