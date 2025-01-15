namespace AleniaAPI.Models
{
    public class Etablissement
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Mission> Missions { get; set; }
    }
}
