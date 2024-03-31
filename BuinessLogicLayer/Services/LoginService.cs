using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public class LoginService : ILoginService
    {
        private readonly IBarberRepository _barberRepository;
        private readonly IClientRepository _clientRepository;
        readonly HashAlgorithm sha = SHA256.Create();
        byte[] hashedPassword;

        public LoginService(IClientRepository clientRepository, IBarberRepository barberRepository)
        {
            _clientRepository = clientRepository;
            _barberRepository = barberRepository;
        }
        public int Login(string email, string password)
        {
            int result = -1;
            //var client = _clientRepository.GetClients().First(x => x.Email == email);
            var client = _clientRepository.GetClientByEmail(email);

            var bytes = Encoding.ASCII.GetBytes(password);

            hashedPassword = sha.ComputeHash(bytes);

            if (client != null)
            {
                if (client.Password.Equals(Encoding.ASCII.GetString(hashedPassword)))
                {
                    result = 1;
                }
                else
                    result = 0;
               
            }
            else
            {
                var barber = _barberRepository.GetBarbers().First(x => x.Email == email);
                if (barber != null)
                {
                    if (barber.Password.Equals(Encoding.ASCII.GetString(hashedPassword)))
                        result = 2;
                    else
                        result = 0;
                }

            }

            return result;
        }
    }
}
