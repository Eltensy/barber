using BuinessLogicLayer.DTOs;

namespace BuinessLogicLayer.Services
{
    public interface IVisitService
    {
        Task<IEnumerable<VisitDto>> GetVisits();
        Task<VisitDto?> GetVisitByID(int visitId);
        Task InsertVisit(VisitDto visitDto);
        Task DeleteVisit(int visitId);
        Task UpdateVisit(VisitDto visitDto);
    }
}
