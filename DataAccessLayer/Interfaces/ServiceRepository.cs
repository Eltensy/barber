using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class ServiceRepository : IServiceRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public ServiceRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Service> GetServices()
        {
            return _context.Services.ToList();
        }

        public Service GetServiceByID(int serviceId)
        {
            return _context.Services.Find(serviceId);
        }

        public void InsertService(Service service)
        {
            _context.Services.Add(service);
        }

        public void DeleteService(int serviceId)
        {
            Service service = _context.Services.Find(serviceId);
            _context.Services.Remove(service);
        }

        public void UpdateService(Service service)
        {
            _context.Entry(service).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
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
