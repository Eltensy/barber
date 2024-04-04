using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BuinessLogicLayer.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task<List<ClientDto>> GetClients()
        {
            var clients = await _clientRepository.GetClients();
            var clientsDtos = from client in clients
                              select new ClientDto()
                              {
                                  Id = client.Id,
                                  Name = client.Name,
                                  Surname = client.Surname,
                                  Phone = client.Phone,
                                  Email = client.Email,
                                  PasswordHash = client.PasswordHash
                              };

            return clientsDtos.ToList();
        }
        public async Task<ClientDto?> GetClientById(int clientId)
        {
            var client = await _clientRepository.GetClientByID(clientId);

            ClientDto? clientDto = null;
            if (null != client)
            {
                clientDto = new ClientDto()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                    Phone = client.Phone,
                    Email = client.Email,
                    PasswordHash = client.PasswordHash
                };
            }
            return clientDto;
        }
        public async Task InsertClient(ClientDto clientDto)
        {
            Client client = new Client()
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Surname = clientDto.Surname,
                Phone = clientDto.Phone,
                Email = clientDto.Email,
                PasswordHash = clientDto.PasswordHash
            };
            await _clientRepository.InsertClient(client);
        }
        public async Task DeleteClient(int clientId)
        {
            await _clientRepository.DeleteClient(clientId);
        }
        public async Task UpdateClient(ClientDto clientDto)
        {
            Client client = new Client()
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Surname = clientDto.Surname,
                Phone = clientDto.Phone,
                Email = clientDto.Email,
                PasswordHash = clientDto.PasswordHash
            };

            await _clientRepository.UpdateClient(client);
        }
        public async Task<ClientDto?> GetClientByEmail(string email)
        {
            Client? client = await _clientRepository.GetClientByEmail(email);
            ClientDto? clientDto = null;
            if (client != null)
            {
                clientDto = new ClientDto()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                    Phone = client.Phone,
                    Email = client.Email,
                    PasswordHash = client.PasswordHash
                };
            }
            return clientDto;
        }
    }
}
