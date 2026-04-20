using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<int> AdminAddBarber(RegistrationDto registrationDto);
    }
}
