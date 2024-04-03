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
        private readonly DataContext _context;

        private bool _disposed = false;

        public ScheduleRepository(DataContext context) 
        {
            this._context = context;
        }

        public async Task<IEnumerable<Schedule>> GetSchedules()
        {
            return await _context.Schedules.ToListAsync();
        }

        public async Task<Schedule?> GetScheduleByID(int scheduleId)
        {
            return await _context.Schedules.FindAsync(scheduleId);
        }

        public async Task InsertSchedule(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            await Save();
        }

        public async Task DeleteSchedule(int scheduleId)
        {
            Schedule? schedule = await _context.Schedules.FindAsync(scheduleId);
            if(null != schedule) _context.Schedules.Remove(schedule);
            await Save();
        }

        public async Task UpdateSchedule(Schedule schedule)
        {
            _context.Entry(schedule).State = EntityState.Modified;
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
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
