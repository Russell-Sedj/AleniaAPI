namespace AleniaAPI.DTOs
{
    public class CandidatureDto
    {
        public Guid Id { get; set; }
        public Guid MissionId { get; set; }
        public string MissionPoste { get; set; } = string.Empty;
        public string MissionEtablissement { get; set; } = string.Empty;
        public Guid InterimaireId { get; set; }
        public string InterimaireNom { get; set; } = string.Empty;
        public string InterimairePrenom { get; set; } = string.Empty;
        public string Statut { get; set; } = string.Empty;
        public DateTime DateCandidature { get; set; }
        public List<string> HorairesChoisis { get; set; } = new();
    }

    public class CreateCandidatureDto
    {
        public Guid MissionId { get; set; }
        public Guid InterimaireId { get; set; }
        public List<string> HorairesChoisis { get; set; } = new();
    }

    public class UpdateCandidatureStatutDto
    {
        public required string Statut { get; set; } // "En cours", "Acceptée", "Refusée"
    }
}
