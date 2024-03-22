namespace barber.Entities
{

    public enum Enum_DayOfWeek
    {
        Monday,
        Tuesday, 
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }



    public class Schedule
    {
        public int Id { get; set; }
        public int fk_BarberId { get; set; }
        public Enum_DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}
    }
}
