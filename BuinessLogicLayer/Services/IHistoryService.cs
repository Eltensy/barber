using BuinessLogicLayer.DTOs;

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
