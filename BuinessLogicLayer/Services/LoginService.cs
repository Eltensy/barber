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
        private readonly IBarberService _barberService;
        private readonly IClientService _clientService;


        public LoginService(IClientService clientService, IBarberService barberService)
        {
            _clientService = clientService;
            _barberService = barberService;
        }
        public async Task<int> Login(string email, string password)
        {
            int result = -1;
            string storedPassword;

            var client = await _clientService.GetClientByEmail(email);
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
                var barber = await _barberService.GetBarberByEmail(email);
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
