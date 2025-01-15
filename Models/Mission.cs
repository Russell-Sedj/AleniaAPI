namespace AleniaAPI.Models
{
    public class Mission
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        public Guid EtablissementId { get; set; }
        public Etablissement? Etablissement { get; set; } // added this ? to make it facultative
    }
}
