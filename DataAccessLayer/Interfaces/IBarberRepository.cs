using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IBarberRepository : IDisposable
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
