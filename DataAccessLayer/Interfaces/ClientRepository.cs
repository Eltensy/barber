using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class ClientRepository : IClientRepository, IDisposable
    {
        private readonly DataContext _context;

        private bool _disposed = false;

        public ClientRepository(DataContext context) 
        {
            this._context = context;
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
            if(null != client) _context.Clients.Remove(client);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
