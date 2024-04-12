namespace BusinessLogicLayer.DTOs
{
    public class BarberDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? PhotoUri { get; set; }
        public string? Description { get; set; }
        public string? PortfolioUri { get; set; }
    }
}
