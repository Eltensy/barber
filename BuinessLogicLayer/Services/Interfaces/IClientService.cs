using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetClients();
        Task<ClientDto?> GetClientById(string clientId);
        //Task InsertClient(ClientDto clientDto);
        //Task DeleteClient(int clientId);
        //Task UpdateClient(ClientDto clientDto);
        Task<ClientDto?> GetClientByEmail(string email);
    }
}
