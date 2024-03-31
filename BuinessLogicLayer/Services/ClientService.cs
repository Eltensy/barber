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
        public List<ClientDto> GetClients()
        {
            var clients = _clientRepository.GetClients();
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
        public ClientDto GetClientById(int clientId)
        {
            var client = _clientRepository.GetClientByID(clientId);
            var clientDto = new ClientDto()
            {
                Id = client.Id,
                Name = client.Name,
                Surname = client.Surname,
                Phone = client.Phone,
                Email = client.Email,
                Password = client.Password
            };
            return clientDto;
        }
        public void InsertClient(ClientDto clientDto)
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
            _clientRepository.InsertClient(client);
            _clientRepository.Save();
        }
        public void DeleteClient(int clientId)
        {
            _clientRepository.DeleteClient(clientId);
            _clientRepository.Save();
        }
        public void UpdateClient(ClientDto clientDto)
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

            _clientRepository.UpdateClient(client);
            _clientRepository.Save();
        }
        public ClientDto? GetClientByEmail(string email)
        {
            Client? client = _clientRepository.GetClientByEmail(email);
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
