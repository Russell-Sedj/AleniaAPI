namespace AleniaAPI.DTOs
{
    public class UtilisateurDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime DateCreation { get; set; }
    }

    public class LoginDto
    {
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
    }

    public class RegisterDto
    {
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
        public required string ConfirmMotDePasse { get; set; }
    }

    public class ChangePasswordDto
    {
        public required string AncienMotDePasse { get; set; }
        public required string NouveauMotDePasse { get; set; }
        public required string ConfirmNouveauMotDePasse { get; set; }
    }

    // SUPPRIMEZ CETTE SECTION - EtablissementDto existe déjà dans EtablissementDto.cs
    // public class EtablissementDto { ... }

    public class CreateEtablissementDto
    {
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
        public required string ConfirmMotDePasse { get; set; }
        public required string Nom { get; set; }
        public required string Responsable { get; set; }
        public required string Adresse { get; set; }
        public required string Telephone { get; set; }
        public required string TypeEtablissement { get; set; }
        public required string NumeroSiret { get; set; }
        public string? Description { get; set; }
    }

    public class LoginEtablissementDto
    {
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
    }
}
