using DataAccessLayer.Entities;

namespace DataAccessLayer.Repositories.Interfaces
{
    public interface IBarberRepository
    {
        Task<IEnumerable<Barber>> GetBarbers();
        Task<Barber?> GetBarberByID(string barberId);
        Task InsertBarber(Barber barber);
        Task DeleteBarber(string barberId);
        Task UpdateBarber(Barber barber);
        Task<Barber?> GetBarberByEmail(string email);
        Task Save();
    }
}
