using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetClientByID(int clientId)
        {
            return await _context.Clients.FindAsync(clientId);
        }

        public async Task InsertClient(Client client)
        {
            await _context.Clients.AddAsync(client);
            await Save();
        }

        public async Task DeleteClient(int clientId)
        {
            Client? client = await _context.Clients.FindAsync(clientId);
            if (null != client) _context.Clients.Remove(client);
            await Save();
        }

        public async Task UpdateClient(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await Save();
        }

        public async Task<Client?> GetClientByEmail(string email)
        {
            Client? client = await _context.Clients.SingleOrDefaultAsync(x => x.Email.Equals(email));
            return client;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
