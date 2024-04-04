using BuinessLogicLayer.DTOs;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;

namespace BuinessLogicLayer.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public async Task DeleteSchedule(int scheduleId)
        {
            await _scheduleRepository.DeleteSchedule(scheduleId);
        }

        public async Task<ScheduleDto?> GetScheduleByID(int scheduleId)
        {
            Schedule? schedule = await _scheduleRepository.GetScheduleByID(scheduleId);
            ScheduleDto? scheduleDto = null;
            if (schedule != null) 
            {
                scheduleDto = new ScheduleDto()
                {
                    Id = schedule.Id,
                    fk_BarberId = schedule.fk_BarberId,
                    DayOfWeek = (DTOs.Enum_DayOfWeek)schedule.DayOfWeek,
                    StartTime = schedule.StartTime,
                    EndTime = schedule.EndTime
                };
            }
            return scheduleDto;
        }

        public async Task<List<ScheduleDto>> GetSchedules()
        {
            var schedules = await _scheduleRepository.GetSchedules();
            var schedulesDtos = from schedule in schedules
                                    select new ScheduleDto()
                                    {
                                        Id = schedule.Id,
                                        fk_BarberId = schedule.fk_BarberId,
                                        DayOfWeek = (DTOs.Enum_DayOfWeek)schedule.DayOfWeek,
                                        StartTime = schedule.StartTime,
                                        EndTime = schedule.EndTime
                                    };
            return schedulesDtos.ToList();
        }

        public async Task InsertSchedule(ScheduleDto scheduleDto)
        {
            Schedule schedule = new Schedule()
            {
                Id = scheduleDto.Id,
                fk_BarberId = scheduleDto.fk_BarberId,
                DayOfWeek = (DataAccessLayer.Entities.Enum_DayOfWeek)scheduleDto.DayOfWeek,
                StartTime = scheduleDto.StartTime,
                EndTime = scheduleDto.EndTime
            };
            await _scheduleRepository.InsertSchedule(schedule);
        }

        public async Task UpdateSchedule(ScheduleDto scheduleDto)
        {
            Schedule schedule = new Schedule()
            {
                Id = scheduleDto.Id,
                fk_BarberId = scheduleDto.fk_BarberId,
                DayOfWeek = (DataAccessLayer.Entities.Enum_DayOfWeek)scheduleDto.DayOfWeek,
                StartTime = scheduleDto.StartTime,
                EndTime = scheduleDto.EndTime
            };
            await _scheduleRepository.UpdateSchedule(schedule);
        }
    }
}
