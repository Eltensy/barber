using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public class RegisterService : IRegisterService
    {
        //private readonly IClientRepository _clientRepository;
        private readonly IClientService _clientService;
        readonly HashAlgorithm sha = SHA256.Create();
        byte[] hashedPassword;
        public RegisterService(IClientService clientService)
        {
            _clientService = clientService;
        }
        //public int Register(string name, string surname, string phone, string email, string password)
        public int Register(ClientDto clientDto)
        {
            int result = -1;

            var existingClient = _clientService.GetClientByEmail(clientDto.Email);

            if (existingClient == null)
            {
                //byte[] bytes = Encoding.ASCII.GetBytes(clientDto.Password);
                byte[] bytes = Encoding.UTF8.GetBytes(clientDto.Password);



                hashedPassword = sha.ComputeHash(bytes);
                string encryptedPass = "bros immaculate";
                ClientDto newClient = new ClientDto()
                {
                    Name = clientDto.Name,
                    Surname = clientDto.Surname,
                    Phone = clientDto.Phone,
                    Email = clientDto.Email,
                    Password = Encoding.ASCII.GetString(hashedPassword)
                    //Password = encryptedPass
                };
                _clientService.InsertClient(newClient);
                result = 0;

            }

            return result;
        }
    }
}
