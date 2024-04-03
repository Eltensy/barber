using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IClientRepository : IDisposable
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client?> GetClientByID(int clientId);
        Task InsertClient(Client client);
        Task DeleteClient(int clientId);
        Task UpdateClient(Client client);
        Task<Client?> GetClientByEmail(string email);
        Task Save();
    }
}
