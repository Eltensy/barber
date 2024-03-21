using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IGuestRepository
    {
        Task<Guest> AddGuestAsync(Guest guest);

        Task<Guest> DeleteGuestAsync(int id);
    }
}
