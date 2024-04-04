using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Service>> GetServices()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetServiceByID(int serviceId)
        {
            return await _context.Services.FindAsync(serviceId);
        }

        public async Task InsertService(Service service)
        {
            await _context.Services.AddAsync(service);
            await Save();
        }

        public async Task DeleteService(int serviceId)
        {
            Service? service = await _context.Services.FindAsync(serviceId);
            if (null != service) _context.Services.Remove(service);
            await Save();
        }

        public async Task UpdateService(Service service)
        {
            _context.Entry(service).State = EntityState.Modified;
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
