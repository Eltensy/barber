using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IRegisterService
    {
        Task<int> Register(ClientDto clientDto);
        Task<int> BarberRegister(BarberDto barberDto);
        Task<int> AdminRegister(AdminDto adminDto);
    }
}
