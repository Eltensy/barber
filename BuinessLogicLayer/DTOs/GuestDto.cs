namespace BuinessLogicLayer.DTOs
{
    public class GuestDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Surname { get; set; }
        public required string Phone { get; set; }
    }
}
