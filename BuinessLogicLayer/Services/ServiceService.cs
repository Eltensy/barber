using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BuinessLogicLayer.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task DeleteService(int serviceId)
        {
            await _serviceRepository.DeleteService(serviceId);
        }

        public async Task<ServiceDto?> GetServiceByID(int serviceId)
        {
            Service? service = await _serviceRepository.GetServiceByID(serviceId);
            ServiceDto? serviceDto = null;
            if(service != null) 
            {
                serviceDto = new ServiceDto()
                {
                    Id = service.Id,
                    fk_BarberId = service.fk_BarberId,
                    Title = service.Title,
                    Description = service.Description,
                    Duration = service.Duration,
                    Price = service.Price
                };
            }
            return serviceDto;
        }

        public async Task<List<ServiceDto>> GetServices()
        {
            var services = await _serviceRepository.GetServices();
            var servicesDtos = from service in services
                               select new ServiceDto()
                               {
                                   Id = service.Id,
                                   fk_BarberId = service.fk_BarberId,
                                   Title = service.Title,
                                   Description = service.Description,
                                   Duration = service.Duration,
                                   Price = service.Price
                               };
            return servicesDtos.ToList();
        }

        public async Task InsertService(ServiceDto serviceDto)
        {
            Service service = new Service()
            {
                Id = serviceDto.Id,
                fk_BarberId = serviceDto.fk_BarberId,
                Title = serviceDto.Title,
                Description = serviceDto.Description,
                Duration = serviceDto.Duration,
                Price = serviceDto.Price
            };
            await _serviceRepository.InsertService(service);
        }

        public async Task UpdateService(ServiceDto serviceDto)
        {
            Service service = new Service()
            {
                Id = serviceDto.Id,
                fk_BarberId = serviceDto.fk_BarberId,
                Title = serviceDto.Title,
                Description = serviceDto.Description,
                Duration = serviceDto.Duration,
                Price = serviceDto.Price
            };
            await _serviceRepository.UpdateService(service);
        }
    }
}
