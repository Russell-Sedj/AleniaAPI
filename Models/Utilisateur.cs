namespace AleniaAPI.Models
{
    public class Utilisateur
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
