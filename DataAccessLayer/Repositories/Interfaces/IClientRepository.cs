using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client?> GetClientByID(string clientId);
        Task InsertClient(Client client);
        Task DeleteClient(string clientId);
        Task UpdateClient(Client client);
        Task<Client?> GetClientByEmail(string email);
        Task Save();
    }
}
