using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class ServiceRepository : IServiceRepository, IDisposable
    {
        private readonly DataContext _context;

        private bool _disposed = false;

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
            if(null != service) _context.Services.Remove(service);
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

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
