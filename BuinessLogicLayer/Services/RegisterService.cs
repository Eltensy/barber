using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BCrypt;

namespace BuinessLogicLayer.Services
{
    public class RegisterService : IRegisterService
    {
        //private readonly IClientRepository _clientRepository;
        private readonly IClientService _clientService;

        public RegisterService(IClientService clientService)
        {
            _clientService = clientService;
        }

        public int Register(ClientDto clientDto)
        {
            int result = -1;
            string hashedPassword;

            var existingClient = _clientService.GetClientByEmail(clientDto.Email);

            if (existingClient == null)
            {
                hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(clientDto.Password);

                ClientDto newClient = new ClientDto()
                {
                    Name = clientDto.Name,
                    Surname = clientDto.Surname,
                    Phone = clientDto.Phone,
                    Email = clientDto.Email,
                    Password = hashedPassword
                };

                _clientService.InsertClient(newClient);

                result = 0;
            }

            return result;
        }
    }
}
