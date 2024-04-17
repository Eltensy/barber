namespace BusinessLogicLayer.DTOs
{
    public class VisitExtDto
    {
        public int Id { get; set; }
        public int? fk_ClientId { get; set; }
        public int? fk_GuestId { get; set; }
        public int fk_BarberId { get; set; }
        public int fk_ServiceId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public string? VisitorFullName { get; set; }
        public string? ServiceTitle { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
