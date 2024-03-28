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
        private DataContext _context;

        private bool _disposed = false;

        public ClientRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        public Client GetClientByID(int clientId)
        {
            return _context.Clients.Find(clientId);
        }

        public void InsertClient(Client client)
        {
            _context.Clients.Add(client);
        }

        public void DeleteClient(int clientId)
        {
            Client client = _context.Clients.Find(clientId);
            _context.Clients.Remove(client);
        }

        public void UpdateClient(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
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
