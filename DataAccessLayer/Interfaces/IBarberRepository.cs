using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IBarberRepository
    {
        Task<IEnumerable<Barber>> GetBarbers();
        Task<Barber?> GetBarberByID(int barberId);
        Task InsertBarber(Barber barber);
        Task DeleteBarber(int barberId);
        Task UpdateBarber(Barber barber);
        Task<Barber?> GetBarberByEmail(string email);
        Task Save();
    }
}
