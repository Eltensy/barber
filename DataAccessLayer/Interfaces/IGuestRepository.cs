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
        Task<IEnumerable<Guest>> GetGuests();
        Task<Guest?> GetGuestByID(int guestId);
        Task InsertGuest(Guest guest);
        Task DeleteGuest(int guestId);
        Task UpdateGuest(Guest guest);
        Task Save();
    }
}
