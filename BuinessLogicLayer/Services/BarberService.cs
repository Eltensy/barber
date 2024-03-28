using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public class BarberService : IBarberService
    {
        private readonly BarberRepository _barberRepository;

        public BarberService(BarberRepository barberRepository)
        {
            _barberRepository = barberRepository;
        }

        public void DeleteBarber(int barberId)
        {
            _barberRepository.DeleteBarber(barberId);
            _barberRepository.Save();
        }

        public BarberDto GetBarberById(int barberId)
        {
            var barber = _barberRepository.GetBarberByID(barberId);
            var barberDto = new BarberDto()
            {
                Id = barber.Id,
                Name = barber.Name,
                Surname = barber.Surname,
                Phone = barber.Phone,
                Email = barber.Email,
                Password = barber.Password
            };
            return barberDto;
        }

        public List<BarberDto> GetBarbers()
        {
            var barbers = _barberRepository.GetBarbers();
            var barbersDtos = from barber in barbers
                              select new BarberDto()
                              {
                                  Id = barber.Id,
                                  Name = barber.Name,
                                  Surname = barber.Surname,
                                  Phone = barber.Phone,
                                  Email = barber.Email,
                                  Password = barber.Password
                              };
            return barbersDtos.ToList();
        }

        public void InsertBarber(BarberDto barberDto)
        {
            Barber barber = new Barber()
            {
                Id = barberDto.Id,
                Name = barberDto.Name,
                Surname = barberDto.Surname,
                Phone = barberDto.Phone,
                Email = barberDto.Email,
                Password = barberDto.Password
            };
            _barberRepository.InsertBarber(barber);
            _barberRepository.Save();
        }

        public void UpdateBarber(BarberDto barberDto)
        {
            Barber barber = new Barber()
            {
                Id = barberDto.Id,
                Name = barberDto.Name,
                Surname = barberDto.Surname,
                Phone = barberDto.Phone,
                Email = barberDto.Email,
                Password = barberDto.Password
            };

            _barberRepository.UpdateBarber(barber);
            _barberRepository.Save();
        }
    }
}
