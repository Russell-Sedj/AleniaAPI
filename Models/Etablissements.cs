namespace AleniaAPI.Models
{
    public class Etablissements
    {
        public int Id { get; set; }
        public string Nom { get; set; }

        // Navigation property
        public ICollection<Missions> Missions { get; set; }
    }
}
