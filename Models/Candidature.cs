namespace AleniaAPI.Models
{
    public class Candidature
    {
        public Guid Id { get; set; }
        public Guid MissionId { get; set; } // foreign key
        public Mission? Mission { get; set; }
        public Guid InterimaireId { get; set; } // foreign key
        public Interimaire? Interimaire { get; set; }
        public string Statut { get; set; } // Enum : "En cours", "Acceptée", "Refusée" // par defaut c'est En cours
        public DateTime DateCandidature { get; set; } // Automatiquement la date du jour
        public List<string> HorairesChoisis { get; set; } // Horaires sélectionnés par l'intérimaire
    }
}
