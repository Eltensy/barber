using BuinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public interface IHistoryService
    {
        Task<List<HistoryDto>> GetHistorys();
        Task<HistoryDto?> GetHistoryByID(int historyId);
        Task InsertHistory(HistoryDto historyDto);
        Task DeleteHistory(int historyId);
        Task UpdateHistory(HistoryDto historyDto);
    }
}
