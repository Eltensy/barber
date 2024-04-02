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
        private readonly IBarberService _barberService;
        private readonly IClientService _clientService;
        readonly HashAlgorithm sha = SHA256.Create();
        byte[] hashedPassword;

        public LoginService(IClientService clientService, IBarberService barberService)
        {
            _clientService = clientService;
            _barberService = barberService;
        }
        public async Task<int> Login(string email, string password)
        {
            int result = -1;
            var client = await _clientService.GetClientByEmail(email);

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
                var barber = await _barberService.GetBarberByEmail(email);
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
