namespace AleniaAPI.Models
{
    public class Etablissement : Utilisateur
    {
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string? Telephone { get; set; }
        public string? TypeEtablissement { get; set; } // Hôpital, Clinique, Laboratoire, etc.
        public ICollection<Mission>? Missions { get; set; }
    }
}
