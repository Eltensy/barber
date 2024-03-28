using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public interface IRegisterService
    {
        int Register(string name, string surname, string phone, string email, string password);
    }
}
