using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IServiceRepository : IDisposable
    {
        IEnumerable<Service> GetServices();
        Service GetServiceByID(int serviceId);
        void InsertService(Service service);
        void DeleteService(int serviceId);
        void UpdateService(Service service);
        void Save();
    }
}
