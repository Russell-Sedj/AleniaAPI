using System.ComponentModel.DataAnnotations.Schema;

namespace AleniaAPI.Models
{
    public class Missions
    {
        public int Id { get; set; }
        public string Poste { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public decimal TauxHoraire { get; set; }
        public int EtablissementsId { get; set; }


        // Navigation property
        [ForeignKey("EtablissementsId")]
        public Etablissements Etablissements { get; set; }
    }
}
