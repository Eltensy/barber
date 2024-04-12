using BusinessLogicLayer.DTOs;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IBarberService
    {
        Task<List<BarberDto>> GetBarbers();
        Task<BarberDto?> GetBarberById(int barberId);
        Task InsertBarber(BarberDto barberDto);
        Task DeleteBarber(int barberId);
        Task UpdateBarber(BarberDto barberDto);
        Task<BarberDto?> GetBarberByEmail(string email);
    }
}
