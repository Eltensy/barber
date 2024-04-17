using BusinessLogicLayer.DTOs;

namespace BarberLayered.Models
{
    public class Client
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }

        public Client(ClientDto clientDto)
        {
            Id = clientDto.Id;
            Name = clientDto.Name;
            Surname = clientDto.Surname;
            Phone = clientDto.Phone;
            Email = clientDto.Email;
            PasswordHash = clientDto.PasswordHash;
        }
    }
}
