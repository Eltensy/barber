using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IClientRepository
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
