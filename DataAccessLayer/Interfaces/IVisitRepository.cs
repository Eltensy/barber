using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces
{
    public interface IVisitRepository
    {
        Task<IEnumerable<Visit>> GetVisits();
        Task<Visit?> GetVisitByID(int visitId);
        Task InsertVisit(Visit visit);
        Task DeleteVisit(int visitId);
        Task UpdateVisit(Visit visit);
        Task Save();
    }
}
