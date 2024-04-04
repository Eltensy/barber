using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IRegistrationKeyRepository
    {
        Task<IEnumerable<RegistrationKey>> GetRegistrationKeys();
        Task<RegistrationKey?> GetRegistrationKeyByID(int registrationKeyId);
        Task InsertRegistrationKey(RegistrationKey registrationKey);
        Task DeleteRegistrationKey(int registrationKeyId);
        Task UpdateRegistrationKey(RegistrationKey registrationKey);
        Task Save();
    }
}
