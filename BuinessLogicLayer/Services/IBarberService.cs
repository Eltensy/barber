using BuinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public interface IBarberService
    {
        List<BarberDto> GetBarbers();
        BarberDto GetBarberById(int barberId);
        void InsertBarber(BarberDto barberDto);
        void DeleteBarber(int barberId);
        void UpdateBarber(BarberDto barberDto);
    }
}
