using BuinessLogicLayer.DTOs;

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
