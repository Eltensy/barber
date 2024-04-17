using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;

namespace BarberLayered.Models
{
    public class Visit
    {
        public int Id { get; set; }
        public string VisitorFullName { get; set; }
        public string ServiceTitle { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public Visit(VisitExtDto visitExtDto)
        {
            Id = visitExtDto.Id;
            VisitorFullName = visitExtDto.VisitorFullName;
            ServiceTitle = visitExtDto.ServiceTitle;
            Date = visitExtDto.Date;
            StartTime = visitExtDto.StartTime;
            EndTime = visitExtDto.EndTime;
        }
    }
}
