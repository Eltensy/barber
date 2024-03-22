namespace barber.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public required string Phone { get; set; }
    }
}
