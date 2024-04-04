using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IRegistrationKeyService
    {
        Task<List<RegistrationKeyDto>> GetRegistrationKeys();
        Task<RegistrationKeyDto?> GetRegistrationKeyById(int registrationKeyId);
        Task InsertRegistrationKey(RegistrationKeyDto registrationKeyDto);
        Task DeleteRegistrationKey(int registrationKeyId);
        Task UpdateRegistrationKey(RegistrationKeyDto registrationKeyDto);
    }
}
