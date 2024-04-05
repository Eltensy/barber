using System.ComponentModel.DataAnnotations;

namespace BarberLayered.Models
{
    public class RegisterViewModelWithKey : RegisterViewModel
    {
        //[Required(ErrorMessage = "Registration Key is required.")]
        [Display(Name = "Registration Key")]
        public string RegistrationKey { get; set; }
    }
}
