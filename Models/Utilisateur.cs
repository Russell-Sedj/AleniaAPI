namespace AleniaAPI.Models
{
    public class Utilisateur
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string MotDePassHash { get; set; }
        public required string Role { get; set; }
        public DateTime DateCreation { get; set; }
    }
}
