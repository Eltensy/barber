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
        IEnumerable<Barber> GetBarbers();
        Barber GetBarberByID(int barberId);
        void InsertBarber(Barber barber);
        void DeleteBarber(int barberId);
        void UpdateBarber(Barber barber);
        void Save();
    }
}
