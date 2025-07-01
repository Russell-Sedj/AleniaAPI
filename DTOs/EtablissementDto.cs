namespace AleniaAPI.DTOs
{
    public class EtablissementResponseDto
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string TypeEtablissement { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? NumeroSiret { get; set; }
        public string Responsable { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateInscription { get; set; }
    }

    // Ajoutez cette classe pour résoudre les erreurs
    public class EtablissementDto
    {
        public Guid Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string TypeEtablissement { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? NumeroSiret { get; set; }
        public string Responsable { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime DateCreation { get; set; }
    }
}