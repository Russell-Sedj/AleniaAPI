namespace AleniaAPI.Models
{
    public class Evaluation
    {
        public Guid Id { get; set; }
        public Guid AuteurId { get; set; } // foreign key
        public Utilisateur? Auteur { get; set; }
        public Guid CibleId { get; set; } // foreign key
        public Utilisateur? Cible { get; set; }
        public Guid? MissionId { get; set; } // foreign key et nullable
        public Mission? Mission { get; set; }
        public string Type { get; set; } // "Evaluation Interimaire", "Evaluation Mission", ou "Evaluation Etablissement"
        public int Note { get; set; } // Note de l'évaluation (exemple : 1 à 5)
        public string? Commentaire { get; set; }
        public DateTime Date { get; set; } // Automatiquement date du jour
    }
}
