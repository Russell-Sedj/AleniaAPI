namespace AleniaAPI.Models
{
    public class Etablissement : Utilisateur
    {
        public string Nom { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string TypeEtablissement { get; set; } = string.Empty; // Hôpital, Clinique, Laboratoire, etc.
        public string NumeroSiret { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<Mission>? Missions { get; set; }
    }
}
