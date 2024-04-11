namespace BuinessLogicLayer.Services
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
        
        public async Task<(int, int)> Login(string email, string password)
        {
            var result = (-1, -1);
            string storedPassword;

            var client = await _clientService.GetClientByEmail(email);
            if (client != null)
            {
                storedPassword = client.PasswordHash;
                if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                {
                    result = (1, client.Id);
                }
            }

            if (-1 == result.Item1)
            {
                var barber = await _barberService.GetBarberByEmail(email);
                if (barber != null)
                {
                    storedPassword = barber.PasswordHash;
                    if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                    {
                        result = (2, barber.Id);
                    }
                }
            }

            if (-1 == result.Item1)
            {
                var admin = await _adminService.GetAdminByEmail(email);
                if (admin != null)
                {
                    storedPassword = admin.PasswordHash;
                    if (BCrypt.Net.BCrypt.EnhancedVerify(password, storedPassword))
                    {
                        result = (1, admin.Id);
                    }
                }
            }

            return result;
        }
    }
}
