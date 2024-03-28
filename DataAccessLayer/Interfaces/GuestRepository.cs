using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class GuestRepository
    {
        private DataContext _context;

        public GuestRepository(DataContext context) 
        {
            _context = context;
        }

    }
}
