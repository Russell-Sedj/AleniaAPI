using System.ComponentModel.DataAnnotations;

namespace AleniaAPI.Models.Auth
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string MotDePass { get; set; }
    }
}
