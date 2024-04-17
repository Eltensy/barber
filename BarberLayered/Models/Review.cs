using BusinessLogicLayer.DTOs;

namespace BarberLayered.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int fk_ClientId { get; set; }
        public int fk_BarberId { get; set; }
        public string? Text { get; set; }
        public float Rating { get; set; }
        public DateTime Date { get; set; }

        public Review(ReviewDto reviewDto)
        {
            Id = reviewDto.Id;
            fk_ClientId = reviewDto.fk_ClientId;
            fk_BarberId = reviewDto.fk_BarberId;
            Text = reviewDto.Text;
            Rating = reviewDto.Rating;
            Date = reviewDto.Date;
        }
    }
}
