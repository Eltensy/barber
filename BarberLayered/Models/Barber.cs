using BusinessLogicLayer.DTOs;

namespace BarberLayered.Models
{
    public class Barber
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } // Why is it here?
        public string? PhotoUri { get; set; } // And this
        public string? Description { get; set; }
        public string? PortfolioUri { get; set; } // Also this

        public Barber() { }
        public Barber(BarberDto barberDto)
        {
            Id = barberDto.Id;
            Name = barberDto.Name;
            Surname = barberDto.Surname;
            Phone = barberDto.Phone;
            Email = barberDto.Email;
            PasswordHash = barberDto.PasswordHash;
            PhotoUri = barberDto.PhotoUri;
            Description = barberDto.Description;
            PortfolioUri = barberDto.PortfolioUri;
        }

        public Barber(UserExtDto userExtDto)
        {
            Id = userExtDto.Id;
            Name = userExtDto.Name;
            Surname = userExtDto.Surname;
            Phone = userExtDto.Phone;
            Email = userExtDto.Email;
            PasswordHash = userExtDto.PasswordHash;
            PhotoUri = userExtDto.PhotoUri;
            Description = userExtDto.Description;
            PortfolioUri = userExtDto.PortfolioUri;
        }
    }
}
