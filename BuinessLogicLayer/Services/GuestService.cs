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
            return guestsDtos;
        }

        public GuestDto GetGuestById(int guestId)
        {
            var guest = _guestRepository.GetGuestById(guestId);
            var guestDto = new GuestDto()
            { 
                Id = guest.Id, 
                Name = guest.Name, 
                Surname = guest.Surname, 
                Phone = guest.Phone
            };

            return guestDto;
        }
        public void InsertGuest(Guest guest)
        {
            _guestRepository.InsertGuest(guest);
        }

        public void DeleteGuest(int guestId)
        {
            _guestRepository.DeleteGuest(guestId);
        }


        public void UpdateGuest(Guest guest)
        {
            _guestRepository.UpdateGuest(guest);
        }
    }
}
