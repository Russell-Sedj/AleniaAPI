namespace AleniaAPI.Models
{
    public class Mission
    {
        public Guid Id { get; set; }
        public Guid EtablissementId { get; set; } // foreign key
        public Etablissement? Etablissement { get; set; }
        public required string Poste { get; set; }
        public string Adresse { get; set; } = string.Empty; // Par défaut, même adresse que l'établissement
        public string? Description { get; set; }
        public required decimal TauxHoraire { get; set; } // DOIT être decimal
        public List<string>? Horaires { get; set; } // Liste de chaînes de caractères
        public DateTime DatePublication { get; set; } // Automatiquement la date du jour
        
        // Champs de planification
        public DateTime? DateMission { get; set; } // Date prévue de la mission
        public TimeSpan? HeureDebut { get; set; } // Heure de début
        public TimeSpan? HeureFin { get; set; } // Heure de fin
        public int? DureeHeures { get; set; } // Durée en heures
        public bool EstPlanifiee { get; set; } = false; // Indique si la mission est planifiée
        
        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
