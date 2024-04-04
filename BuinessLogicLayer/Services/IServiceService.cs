using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetServices();
        Task<ServiceDto?> GetServiceByID(int serviceId);
        public async Task<List<ServiceDto>> GetServicesByBarberId(int fkBarberId);
        Task InsertService(ServiceDto serviceDto);
        Task DeleteService(int serviceId);
        Task UpdateService(ServiceDto serviceDto);
    }
}
