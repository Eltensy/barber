using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;

namespace BuinessLogicLayer.Services
{
    public interface IGuestService
    {
        List<GuestDto> GetGuests();
        GuestDto GetGuestById(int guestId);
        void InsertGuest(GuestDto guestDto);
        void DeleteGuest(int guestId);
        void UpdateGuest(GuestDto guestDto);
    }
}
