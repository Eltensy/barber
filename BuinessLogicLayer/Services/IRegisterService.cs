using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IRegisterService
    {
        Task<int> Register(ClientDto clientDto);
    }
}
