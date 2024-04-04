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
                storedPassword = client.PasswordHash;
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
                    storedPassword = barber.PasswordHash;
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
