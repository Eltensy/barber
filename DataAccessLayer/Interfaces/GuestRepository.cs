using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This is the comment from andrew_branch

namespace DataAccessLayer.Interfaces
{
    public class GuestRepository 
    {
        private readonly DataContext _dataContext;

        public GuestRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
    }
}
