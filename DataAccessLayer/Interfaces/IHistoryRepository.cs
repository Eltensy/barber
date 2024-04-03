using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IHistoryRepository : IDisposable
    {
        Task<IEnumerable<History>> GetHistorys();
        Task<History?> GetHistoryByID(int historyId);
        Task InsertHistory(History history);
        Task DeleteHistory(int historyId);
        Task UpdateHistory(History history);
        Task Save();
    }
}
