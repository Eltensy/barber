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
    public class LoginService : ILoginService
    {
        private readonly IBarberRepository _barberRepository;
        private readonly IClientRepository _clientRepository;
        //readonly HashAlgorithm sha = SHA256.Create();
        //byte[] hashedPassword;

        public LoginService(IClientRepository clientRepository, IBarberRepository barberRepository)
        {
            _clientRepository = clientRepository;
            _barberRepository = barberRepository;
        }
        public int Login(string email, string password)
        {
            int result = -1;
            string storedPassword;


            var client = _clientRepository.GetClientByEmail(email);
            if (client != null)
            {
                storedPassword = client.Password;
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                {
                    result = 1;
                }
            }
            else
            {
                var barber = _barberRepository.GetBarbers().First(x => x.Email == email);
                if (barber != null)
                {
                    storedPassword = barber.Password;
                    if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                    {
                        result = 2;
                    }
                }
            }

            return result;
        }
    }
}
