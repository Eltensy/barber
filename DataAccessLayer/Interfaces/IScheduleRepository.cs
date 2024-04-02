using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IScheduleRepository : IDisposable
    {
        Task<IEnumerable<Schedule>> GetSchedules();
        Task<Schedule?> GetScheduleByID(int scheduleId);
        Task InsertSchedule(Schedule schedule);
        Task DeleteSchedule(int scheduleId);
        Task UpdateSchedule(Schedule schedule);
        Task Save();
    }
}
