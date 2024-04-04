namespace BarberLayered.Models
{
    public class AppointmentViewModel
    {
        public int ServiceId { get; set; }
        public List<DateTime>? BookedDates { get; set; }
        public List<DateTime>? AvailableDates { get; set; }
    }
}
