using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Serilog;


namespace BusinessLogicLayer.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly IClientService _clientService;
        private readonly IBarberService _barberService;
        private readonly IAdminService _adminService;

        public RegisterService(IClientService clientService, IBarberService barberService, IAdminService adminService)
        {
            _clientService = clientService;
            _barberService = barberService;
            _adminService = adminService;

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

                Log.Information("Successfully registered as client with Email: {Email}", newClient.Email);
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

                Log.Information("Successfully registered as barber with Email: {Email}", newBarber.Email);
            }

            return result;
        }

        public async Task<int> AdminRegister(AdminDto adminDto)
        {
            int result = -1;
            string hashedPassword;

            var existingAdmin = await _adminService.GetAdminByEmail(adminDto.Email);

            if (existingAdmin == null)
            {
                hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(adminDto.PasswordHash);

                AdminDto newAdmin = new AdminDto()
                {
                    Name = adminDto.Name,
                    Surname = adminDto.Surname,
                    Phone = adminDto.Phone,
                    Email = adminDto.Email,
                    PasswordHash = hashedPassword
                };

                await _adminService.InsertAdmin(newAdmin);
                
                result = 0;
                
                Log.Information("Successfully registered as admin with Email: {Email}", newAdmin.Email);
            }

            return result;
        }

    }
}
