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
}
