using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AleniaAPI.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid InterimaireId { get; set; }

        [Required]
        [StringLength(200)]
        public string Expediteur { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Sujet { get; set; } = string.Empty;

        [Required]
        public string Contenu { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Categorie { get; set; } = string.Empty; // mission, administrative, planning, generale, technique

        public bool Important { get; set; } = false;

        public bool Urgent { get; set; } = false;

        [Required]
        public DateTime DateEnvoi { get; set; }

        public bool Lu { get; set; } = false;

        // Relations
        [ForeignKey("InterimaireId")]
        public virtual Interimaire? Interimaire { get; set; }
    }
}
