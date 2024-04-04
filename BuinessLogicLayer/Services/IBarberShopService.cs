using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IBarberShopService
    {
        Task<List<BarberShopDto>> GetBarberShops();
        Task<BarberShopDto?> GetBarberShopById(int barbershopId);
        Task<BarberShopDto?> GetBarberShopFirst();
        Task InsertBarberShop(BarberShopDto barbershopDto);
        Task DeleteBarberShop(int barbershopId);
        Task UpdateBarberShop(BarberShopDto barbershopDto);
    }
}
