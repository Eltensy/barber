using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IGuestRepository : IDisposable
    {
        IEnumerable<Guest> GetGuests();
        Guest GetGuestByID(int guestId);
        void InsertGuest(Guest guest);
        void DeleteGuest(int guestId);
        void UpdateGuest(Guest guest);
        void Save();
    }
}
