using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Interfaces
{
    public class ScheduleRepository : IScheduleRepository, IDisposable
    {
        private DataContext _context;

        private bool _disposed = false;

        public ScheduleRepository(DataContext context) 
        {
            this._context = context;
        }

        public IEnumerable<Schedule> GetSchedules()
        {
            return _context.Schedules.ToList();
        }

        public Schedule GetScheduleByID(int scheduleId)
        {
            return _context.Schedules.Find(scheduleId);
        }

        public void InsertSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
        }

        public void DeleteSchedule(int scheduleId)
        {
            Schedule schedule = _context.Schedules.Find(scheduleId);
            _context.Schedules.Remove(schedule);
        }

        public void UpdateSchedule(Schedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
