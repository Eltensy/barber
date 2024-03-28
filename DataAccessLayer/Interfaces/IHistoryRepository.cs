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
        IEnumerable<History> GetHistorys();
        History GetHistoryByID(int historyId);
        void InsertHistory(History history);
        void DeleteHistory(int historyId);
        void UpdateHistory(History history);
        void Save();
    }
}
