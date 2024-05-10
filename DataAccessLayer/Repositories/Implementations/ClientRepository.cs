using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;
        private readonly IApplicationUsersHelper _applicationUsersHelper;

        public ClientRepository(DataContext context, IApplicationUsersHelper applicationUsersHelper)
        {
            _context = context;
            _applicationUsersHelper = applicationUsersHelper;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            //return await _context.Clients.ToListAsync();

            List<Client> clients = new List<Client>();

            var clientRoles = await _applicationUsersHelper.GetRolesToUsers("Client");
            foreach (var clientRole in clientRoles)
            {
                clients.Add(new Client(clientRole));
            }

            return clients.ToList();
        }

        public async Task<Client?> GetClientByID(string clientId)
        {
            //return await _context.Clients.FindAsync(clientId);

            var appUser = await _context.ApplicationUsers.FindAsync(clientId);
            if (appUser == null)
            {
                return null;
            }
            return new Client(appUser);
        }

        //public async Task InsertClient(Client client)
        //{
        //    await _context.Clients.AddAsync(client);
        //    await Save();
        //}

        //public async Task DeleteClient(int clientId)
        //{
        //    Client? client = await _context.Clients.FindAsync(clientId);
        //    if (null != client) _context.Clients.Remove(client);
        //    await Save();
        //}

        //public async Task UpdateClient(Client client)
        //{
        //    _context.Entry(client).State = EntityState.Modified;
        //    await Save();
        //}

        public async Task<Client?> GetClientByEmail(string email)
        {
            //Client? client = await _context.Clients.SingleOrDefaultAsync(x => x.Email.Equals(email));
            //return client;
            //ApplicationUser? appUser = await _context.ApplicationUsers.SingleOrDefaultAsync(x => x.Email.Equals(email));
            var appUsers = await _applicationUsersHelper.GetRolesToUsers("Client");
            var appUser = appUsers.Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (appUser == null)
            {
                return null;
            }

            return new Client(appUser);
        }

        //public async Task Save()
        //{
        //    await _context.SaveChangesAsync();
        //}
    }
}
