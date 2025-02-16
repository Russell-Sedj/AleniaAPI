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

        // Propriétés spécifiques pour Admin
        public string? Pseudo { get; set; }

        // Propriétés spécifiques pour Etablissement
        public string? Nom { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public string? TypeEtablissement { get; set; }

        // Propriétés spécifiques pour Interimaire
        public string? Prenom { get; set; }
        public List<string>? Competences { get; set; }
        public string? Disponibilites { get; set; }
    }
}
