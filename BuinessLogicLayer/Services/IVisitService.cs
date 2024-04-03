using BuinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
