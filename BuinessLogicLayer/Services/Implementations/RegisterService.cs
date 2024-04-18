using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using Serilog;


namespace BusinessLogicLayer.Services.Implementations
{
    public class RegisterService : IRegisterService
    {
        private readonly IAdminService _adminService;
        private readonly IBarberService _barberService;
        private readonly IClientService _clientService;
        private readonly IRegistrationKeyService _registrationKeyService;

        public RegisterService(IAdminService adminService, IBarberService barberService, IClientService clientService, IRegistrationKeyService registrationKeyService)
        {
            _adminService = adminService;
            _barberService = barberService;
            _clientService = clientService;
            _registrationKeyService = registrationKeyService;
        }

        public async Task<UserExtDto?> Register(RegistrationDto registrationDto)
        {
            UserExtDto? userExtDto = null;
            string hashedPassword;
            var registrationKey = await _registrationKeyService.GetRegistrationKeyById(1);
            if(registrationDto.RegistrationKey == null)
            {
                var existingClient = await _clientService.GetClientByEmail(registrationDto.Email);

                if (existingClient == null)
                {
                    hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(registrationDto.Password);

                    ClientDto newClient = new ClientDto()
                    {
                        Name = registrationDto.Name,
                        Surname = registrationDto.Surname,
                        Phone = registrationDto.Phone,
                        Email = registrationDto.Email,
                        PasswordHash = hashedPassword
                    };

                    await _clientService.InsertClient(newClient);

                    userExtDto = new UserExtDto(newClient);
                    userExtDto.IsRegistrationKeyValid = true;

                    Log.Information("Successfully registered as client with Email: {Email}", newClient.Email);
                }
            }
            else
            {
                if (!registrationDto.RegistrationKey.ToString().Equals(registrationKey.Key.ToString()))
                {
                    userExtDto = new UserExtDto
                    { IsRegistrationKeyValid = false };
                }
                else
                {
                    switch (registrationDto.UserType)
                    {
                        case _UserType.Admin:
                            var existingAdmin = await _adminService.GetAdminByEmail(registrationDto.Email);

                            if (existingAdmin == null)
                            {
                                hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(registrationDto.Password);

                                AdminDto newAdmin = new AdminDto()
                                {
                                    Name = registrationDto.Name,
                                    Surname = registrationDto.Surname,
                                    Phone = registrationDto.Phone,
                                    Email = registrationDto.Email,
                                    PasswordHash = hashedPassword
                                };

                                await _adminService.InsertAdmin(newAdmin);

                                userExtDto = new UserExtDto(newAdmin);
                                userExtDto.IsRegistrationKeyValid = true;

                                Log.Information("Successfully registered as admin with Email: {Email}", newAdmin.Email);
                            }
                            break;
                        case _UserType.Barber:
                            var existingBarber = await _barberService.GetBarberByEmail(registrationDto.Email);

                            if (existingBarber == null)
                            {
                                hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(registrationDto.Password);

                                BarberDto newBarber = new BarberDto()
                                {
                                    Name = registrationDto.Name,
                                    Surname = registrationDto.Surname,
                                    Phone = registrationDto.Phone,
                                    Email = registrationDto.Email,
                                    PasswordHash = hashedPassword
                                };

                                await _barberService.InsertBarber(newBarber);

                                userExtDto = new UserExtDto(newBarber);
                                userExtDto.IsRegistrationKeyValid = true;

                                Log.Information("Successfully registered as barber with Email: {Email}", newBarber.Email);
                            }
                            break;
                    }
                }
            }

            return userExtDto;
        }
    }
}
