using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer.Entities;

namespace BuinessLogicLayer
{
    public interface IGuestService
    {
        Task<Guest> AddGuestAsync(Guest guest);

        Task<Guest> DeleteGuestAsync(int id);
    }
}
