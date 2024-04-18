namespace BusinessLogicLayer.DTOs
{
    public class BarberDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string? PhotoUri { get; set; }
        public string? Description { get; set; }
        public string? PortfolioUri { get; set; }

        public BarberDto() { }

        public BarberDto(RegistrationDto registrationDto, string hashedPassword)
        {
            Name = registrationDto.Name;
            Surname = registrationDto.Surname;
            Phone = registrationDto.Phone;
            Email = registrationDto.Email;
            PasswordHash = hashedPassword;
        }
    }
}
