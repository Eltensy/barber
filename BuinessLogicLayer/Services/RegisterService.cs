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
        private readonly ClientRepository _clientRepository;
        HashAlgorithm sha = SHA256.Create();
        byte[] hashedPassword;
        public RegisterService(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public int Register(string name, string surname, string phone, string email, string password)
        {
            int result = -1;

            var existingClient = _clientRepository.GetClients().Where(x => x.Email == email);

            if (!existingClient.Any())
            {
                //var bytes = Convert.FromBase64String(password);
                byte[] bytes = Encoding.ASCII.GetBytes(password);

                hashedPassword = sha.ComputeHash(bytes);
                Client newClient = new Client()
                {
                    Name = name,
                    Surname = surname,
                    Phone = phone,
                    Email = email,
                    Password = Encoding.ASCII.GetString(hashedPassword)
                };
                _clientRepository.InsertClient(newClient);
                _clientRepository.Save();
                result = 0;

            }

            return result;
        }
    }
}
