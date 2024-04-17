using BusinessLogicLayer.DTOs;
using DataAccessLayer.Entities;

namespace BarberLayered.Models
{
    public class Service
    {
        public int Id { get; set; }
        public int fk_BarberId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public TimeOnly Duration { get; set; }
        public int Price { get; set; }

        public Service(ServiceDto serviceDto)
        {
            Id = serviceDto.Id;
            fk_BarberId = serviceDto.Id;
            Title = serviceDto.Title;
            Description = serviceDto.Description;
            Duration = serviceDto.Duration;
            Price = serviceDto.Price;
        }
    }
}
