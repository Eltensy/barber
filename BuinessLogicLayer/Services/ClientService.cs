using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                                 Password = client.Password
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
                    Password = client.Password
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
                Password = clientDto.Password
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
                Password = clientDto.Password
            };

            await _clientRepository.UpdateClient(client);
        }
        public async Task<ClientDto?> GetClientByEmail(string email)
        {
            Client? client = await _clientRepository.GetClientByEmail(email);
            ClientDto? clientDto = null;
            if(client != null)
            {
                clientDto = new ClientDto()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                    Phone = client.Phone,
                    Email = client.Email,
                    Password = client.Password
                };
            }
            return clientDto;
        }
    }
}
