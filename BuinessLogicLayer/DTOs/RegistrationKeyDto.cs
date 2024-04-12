namespace BusinessLogicLayer.DTOs
{
    public class RegistrationKeyDto
    {
        public int Id { get; set; }
        public required string Key { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
