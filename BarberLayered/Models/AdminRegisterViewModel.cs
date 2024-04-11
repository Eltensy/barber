using System.ComponentModel.DataAnnotations;

namespace BarberLayered.Models
{
    public class AdminRegisterViewModel : RegisterViewModel
    {
        [Required(ErrorMessage = "Registration Key is required for administrators.")]
        [Display(Name = "Registration Key")]
        public string? RegistrationKey { get; set; }
    }
}
