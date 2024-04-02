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
        Task<List<GuestDto>> GetGuests();
        Task<GuestDto?> GetGuestById(int guestId);
        Task InsertGuest(GuestDto guestDto);
        Task DeleteGuest(int guestId);
        Task UpdateGuest(GuestDto guestDto);
    }
}
