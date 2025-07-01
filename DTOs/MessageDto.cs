namespace AleniaAPI.DTOs
{
    public class MessageDto
    {
        public int? Id { get; set; }
        public Guid InterimaireId { get; set; }
        public string Expediteur { get; set; } = string.Empty;
        public string Sujet { get; set; } = string.Empty;
        public string Contenu { get; set; } = string.Empty;
        public string Categorie { get; set; } = string.Empty;
        public bool? Important { get; set; } = false;
        public bool? Urgent { get; set; } = false;
        public DateTime? DateEnvoi { get; set; }
        public bool? Lu { get; set; } = false;
    }
}
