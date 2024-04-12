namespace BusinessLogicLayer.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
    }
}
