using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IVisitRepository : IDisposable
    {
        Task<IEnumerable<Visit>> GetVisits();
        Task<Visit?> GetVisitByID(int visitId);
        Task InsertVisit(Visit visit);
        Task DeleteVisit(int visitId);
        Task UpdateVisit(Visit visit);
        Task Save();
    }
}
