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
        IEnumerable<Schedule> GetSchedules();
        Schedule GetScheduleByID(int scheduleId);
        void InsertSchedule(Schedule schedule);
        void DeleteSchedule(int scheduleId);
        void UpdateSchedule(Schedule schedule);
        void Save();
    }
}
