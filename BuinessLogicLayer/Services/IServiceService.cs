using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetServices();
        Task<ServiceDto?> GetServiceByID(int serviceId);
        Task InsertService(ServiceDto serviceDto);
        Task DeleteService(int serviceId);
        Task UpdateService(ServiceDto serviceDto);
    }
}
