using System.ComponentModel.DataAnnotations;

namespace BarberLayered.Models
{
    public class BarberRegisterViewModel : RegisterViewModel
    {
        [Required(ErrorMessage = "Registration Key is required for barbers.")]
        [Display(Name = "Registration Key")]
        public string? RegistrationKey { get; set; }
    }
}
