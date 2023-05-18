using System.ComponentModel.DataAnnotations;

namespace BadHunter.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{8,}$",
            ErrorMessage = "Password is too simple")]
        public string? Password { get; set; }
    }
}