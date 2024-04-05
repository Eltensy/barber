using BuinessLogicLayer.DTOs;


namespace BuinessLogicLayer.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IClientService _clientService;
        private readonly IBarberService _barberService;

        public RegisterService(IClientService clientService, IBarberService barberService)
        {
            _clientService = clientService;
            _barberService = barberService;
        }
        public async Task<int> Register(ClientDto clientDto)
        {
            int result = -1;
            string hashedPassword;

            var existingClient = await _clientService.GetClientByEmail(clientDto.Email);

            if (existingClient == null)
            {
                hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(clientDto.PasswordHash);

                ClientDto newClient = new ClientDto()
                {
                    Name = clientDto.Name,
                    Surname = clientDto.Surname,
                    Phone = clientDto.Phone,
                    Email = clientDto.Email,
                    PasswordHash = hashedPassword
                };

                await _clientService.InsertClient(newClient);

                result = 0;
            }

            return result;
        }
        public async Task<int> BarberRegister(BarberDto barberDto)
        {
            int result = -1;
            string hashedPassword;

            var existingBarber = await _barberService.GetBarberByEmail(barberDto.Email);

            if (existingBarber == null)
            {
                hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(barberDto.PasswordHash);

                BarberDto newBarber = new BarberDto()
                {
                    Name = barberDto.Name,
                    Surname = barberDto.Surname,
                    Phone = barberDto.Phone,
                    Email = barberDto.Email,
                    PasswordHash = hashedPassword
                };

                await _barberService.InsertBarber(newBarber);

                result = 0;
            }

            return result;
        }
    }
}
