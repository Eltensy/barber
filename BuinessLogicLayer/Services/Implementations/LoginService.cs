using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Serilog;

namespace BusinessLogicLayer.Services.Implementations
{
    public class LoginService : ILoginService
    {
        private readonly IBarberService _barberService;
        private readonly IClientService _clientService;
        private readonly IAdminService _adminService;

        public LoginService(IClientService clientService, IBarberService barberService, IAdminService adminService)
        {
            _clientService = clientService;
            _barberService = barberService;
            _adminService = adminService;
        }

        public async Task<UserExtDto?> Login(string email, string password)
        {
            UserExtDto? userExtDto = null;
            string storedPassword;

            var client = await _clientService.GetClientByEmail(email);
            if (client != null)
            {
                storedPassword = client.PasswordHash;
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                {
                    Log.Information("Successfully logged in as client with Id: {Id}", client.Id);
                    userExtDto = new UserExtDto(client);
                }
            }

            else
            {
                var barber = await _barberService.GetBarberByEmail(email);
                if (barber != null)
                {
                    storedPassword = barber.PasswordHash;
                    if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                    {
                        Log.Information("Successfully logged in as barber with Id: {Id}", barber.Id);
                        userExtDto = new UserExtDto(barber);
                    }
                }
                else
                {
                    var admin = await _adminService.GetAdminByEmail(email);
                    if (admin != null)
                    {
                        storedPassword = admin.PasswordHash;
                        if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                        {
                            Log.Information("Successfully logged in as admin with Id: {Id}", admin.Id);
                            userExtDto = new UserExtDto(admin);
                        }
                    }
                }
            }

            return userExtDto;
        }
    }
}
