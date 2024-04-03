using BuinessLogicLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuinessLogicLayer.Services
{
    public interface IScheduleService
    {
        Task<List<ScheduleDto>> GetSchedules();
        Task<ScheduleDto?> GetScheduleByID(int scheduleId);
        Task InsertSchedule(ScheduleDto scheduleDto);
        Task DeleteSchedule(int scheduleId);
        Task UpdateSchedule(ScheduleDto scheduleDto);
    }
}
