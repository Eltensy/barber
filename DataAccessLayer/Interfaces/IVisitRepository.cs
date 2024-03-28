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
        IEnumerable<Visit> GetVisits();
        Visit GetVisitByID(int visitId);
        void InsertVisit(Visit visit);
        void DeleteVisit(int visitId);
        void UpdateVisit(Visit visit);
        void Save();
    }
}
