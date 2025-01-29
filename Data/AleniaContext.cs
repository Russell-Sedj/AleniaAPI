using AleniaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AleniaAPI.Data
{
    public class AleniaContext : DbContext
    {
        public AleniaContext(DbContextOptions<AleniaContext> options) : base(options) { }

        // DbSet are a collection of entities that can be queried
        // so i let the s at the end of the property name

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Etablissement> Etablissements { get; set; }
        public DbSet<Interimaire> Interimaires { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Utilisateur>()
                .HasDiscriminator<string>("Role")
                .HasValue<Etablissement>("etablissement")
                .HasValue<Interimaire>("interimaire")
                .HasValue<Admin>("admin");

            modelBuilder.Entity<Mission>()
                .HasOne(m => m.Etablissement)
                .WithMany(e => e.Missions)
                .HasForeignKey(m => m.EtablissementId);

            modelBuilder.Entity<Certification>()
                .HasOne(c => c.Interimaire)
                .WithMany(i => i.Certifications)
                .HasForeignKey(c => c.InterimaireId);

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Mission)
                .WithMany(m => m.Candidatures)
                .HasForeignKey(c => c.MissionId);

            modelBuilder.Entity<Candidature>()
                .HasOne(c => c.Interimaire)
                .WithMany(i => i.Candidatures)
                .HasForeignKey(c => c.InterimaireId);

            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.Auteur)
                .WithMany()
                .HasForeignKey(e => e.AuteurId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.Cible)
                .WithMany()
                .HasForeignKey(e => e.CibleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Evaluation>()
                .HasOne(e => e.Mission)
                .WithMany()
                .HasForeignKey(e => e.MissionId);
        }
    }
}
