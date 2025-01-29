namespace AleniaAPI.Models
{
    public class Interimaire : Utilisateur
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string? Adresse { get; set; }
        public string? Telephone { get; set; }
        public List<string>? Competences { get; set; } // Liste des compétences spécifiques
        public string? Disponibilites { get; set; } // Exemple : "Lundi-Vendredi, 8h-18h"
        public ICollection<Certification>? Certifications { get; set; }
        public ICollection<Candidature>? Candidatures { get; set; }
    }
}
