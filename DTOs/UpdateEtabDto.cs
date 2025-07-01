using System.ComponentModel.DataAnnotations;

namespace AleniaAPI.DTOs
{
    public class UpdateEtablissementDto
    {
        [Required]
        public string? Nom { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Adresse { get; set; }

        [Required]
        public string? Telephone { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Siret { get; set; }

        [Required]
        public string? Responsable { get; set; }

        public string? Description { get; set; }
    }
}