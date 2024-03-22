namespace barber.Entities
{
    public class Visit
    {
        public int Id { get; set; }
        public int? fk_CLientId { get; set; }
        public int? fk_GuestId { get; set; } 
        public int fk_BarberId { get; set; } 
        public int fk_ServiceId { get; set; }
        public DateOnly Date {  get; set; }
        public TimeOnly Time {  get; set; }
    }
}
