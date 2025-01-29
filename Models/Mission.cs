namespace AleniaAPI.Models
{
    public class Mission
    {
        public Guid Id { get; set; }
        public Guid EtablissementId { get; set; } // foreign key
        public Etablissement? Etablissement { get; set; }
        public string Adresse { get; set; } // Par défaut, même adresse que l'établissement
        public string? Description { get; set; }
        public List<string>? Horaires { get; set; } // Liste de chaînes de caractères (exemple : "Lundi - 15h30 - 23h45")
        public ICollection<Candidature>? Candidatures { get; set; }

    }
}
