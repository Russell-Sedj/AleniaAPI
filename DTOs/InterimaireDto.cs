namespace AleniaAPI.DTOs
{
    public class InterimaireDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public List<string>? Competences { get; set; }
        public string? Disponibilites { get; set; }
        public DateTime DateCreation { get; set; }
    }    public class CreateInterimaireDto
    {
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
        public required string ConfirmMotDePasse { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public List<string>? Competences { get; set; }
        public string? Disponibilites { get; set; }
    }

    public class UpdateInterimaireDto
    {
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public List<string>? Competences { get; set; }
        public string? Disponibilites { get; set; }
    }
}
