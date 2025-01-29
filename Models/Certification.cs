namespace AleniaAPI.Models
{
    public class Certification
    {
        public Guid Id { get; set; }
        public Guid InterimaireId { get; set; } // foreign key
        public Interimaire? Interimaire { get; set; }
        public string Type { get; set; } // Diplôme, Accréditation, etc.
        public string DocumentUrl { get; set; }
        public string Statut { get; set; } // Enum : "En cours", "Validé", "Refusé"
        public DateTime? DateValidation { get; set; }
    }
}
