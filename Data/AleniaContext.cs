using AleniaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AleniaAPI.Data
{
    public class AleniaContext : DbContext
    {
        public AleniaContext(DbContextOptions<AleniaContext> options) : base(options) { }

        // DbSet are a collection of entities that can be queried
        // so i let the s at the end of the property name
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Etablissement)
                .WithMany(e => e.Missions)
                .HasForeignKey(m => m.EtablissementId);
        }
    }
}
