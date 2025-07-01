namespace AleniaAPI.DTOs
{
    public class MissionDto
    {
        public Guid Id { get; set; }
        public Guid EtablissementId { get; set; }
        public string EtablissementNom { get; set; } = string.Empty;
        public string Poste { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal TauxHoraire { get; set; } // DOIT être decimal
        public List<string>? Horaires { get; set; }
        public DateTime DatePublication { get; set; }
        public int NombreCandidatures { get; set; }
    }

    public class CreateMissionDto
    {
        public Guid EtablissementId { get; set; }
        public required string Poste { get; set; }
        public required string Adresse { get; set; }
        public string? Description { get; set; }
        public required decimal TauxHoraire { get; set; } // DOIT être decimal
        public List<string>? Horaires { get; set; }
    }

    public class UpdateMissionDto
    {
        public string? Poste { get; set; }
        public string? Adresse { get; set; }
        public string? Description { get; set; }
        public decimal? TauxHoraire { get; set; } // DOIT être decimal
        public List<string>? Horaires { get; set; }
    }

    public class MissionDetailDto : MissionDto
    {
        public List<CandidatureDto>? Candidatures { get; set; }
    }
}
