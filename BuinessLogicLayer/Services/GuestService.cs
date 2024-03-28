using BuinessLogicLayer.DTOs;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public class GuestService : IGuestService
    {
        private readonly GuestRepository _guestRepository;

        public GuestService(GuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }
        public List<GuestDto> GetGuests()
        {
            var guests = _guestRepository.GetGuests();
            var guestsDtos = from guest in guests
                             select new GuestDto()
                             {
                                 Id = guest.Id,
                                 Name = guest.Name,
                                 Surname = guest.Surname,
                                 Phone = guest.Phone
                             };
            return guestsDtos.ToList();
        }
        public GuestDto GetGuestById(int guestId)
        {
            var guest = _guestRepository.GetGuestByID(guestId);
            var guestDto = new GuestDto()
            { 
                Id = guest.Id, 
                Name = guest.Name, 
                Surname = guest.Surname, 
                Phone = guest.Phone
            };
            return guestDto;
        }
        public void InsertGuest(GuestDto guestDto)
        {
            Guest guest = new Guest()
            {
                Id = guestDto.Id,
                Name = guestDto.Name,
                Surname = guestDto.Surname,
                Phone = guestDto.Phone
            };
            _guestRepository.InsertGuest(guest);
            _guestRepository.Save();
        }
        public void DeleteGuest(int guestId)
        {
            _guestRepository.DeleteGuest(guestId);
            _guestRepository.Save();
        }
        public void UpdateGuest(GuestDto guestDto)
        {
            Guest guest = new Guest()
            {
                Id = guestDto.Id,
                Name = guestDto.Name,
                Surname = guestDto.Surname,
                Phone = guestDto.Phone
            };

            _guestRepository.UpdateGuest(guest);
            _guestRepository.Save();
        }
    }
}
