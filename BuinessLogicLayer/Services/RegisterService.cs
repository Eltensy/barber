using BuinessLogicLayer.DTOs;


namespace BuinessLogicLayer.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IClientService _clientService;

        public RegisterService(IClientService clientService)
        {
            _clientService = clientService;
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
    }
}
