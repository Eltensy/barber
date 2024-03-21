using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer
{
    public class GuestService : IGuestService
    {
        private readonly GuestRepository _guestRepository;

        public GuestService(GuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public Task<Guest> AddGuestAsync(Guest guest)
        {
            throw new NotImplementedException();
        }

        public Task<Guest> DeleteGuestAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
