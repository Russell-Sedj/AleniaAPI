using System.ComponentModel.DataAnnotations;

namespace AleniaAPI.Models.Auth
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "L'email est requis")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères")]
        public string MotDePass { get; set; }

        [Required(ErrorMessage = "Le rôle est requis")]
        [RegularExpression("^(admin|etablissement|interimaire)$",
         ErrorMessage = "Le rôle doit être 'admin', 'etablissement' ou 'interimaire'")]
        public string Role { get; set; }
    }
}
