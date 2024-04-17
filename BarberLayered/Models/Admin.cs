using BusinessLogicLayer.DTOs;

namespace BarberLayered.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Admin(AdminDto adminDto)
        {
            Id = adminDto.Id;
            Name = adminDto.Name;
            Surname = adminDto.Surname;
            Phone = adminDto.Phone;
            Email = adminDto.Email;
            PasswordHash = adminDto.PasswordHash;
        }
    }
}