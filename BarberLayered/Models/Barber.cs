namespace BarberLayered.Models
{
    public class Barber
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; } // Why is it here?
        public string? PhotoUri { get; set; } // And this
        public string? Description { get; set; }
        public string? PortfolioUri { get; set; } // Also this
    }
}
