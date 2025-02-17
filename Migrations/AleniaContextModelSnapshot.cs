﻿// <auto-generated />
using System;
using AleniaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AleniaAPI.Migrations
{
    [DbContext(typeof(AleniaContext))]
    partial class AleniaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AleniaAPI.Models.Candidature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCandidature")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HorairesChoisis")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("InterimaireId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MissionId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("InterimaireId");

                    b.HasIndex("MissionId");

                    b.ToTable("Candidatures");
                });

            modelBuilder.Entity("AleniaAPI.Models.Certification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DateValidation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DocumentUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("InterimaireId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Statut")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("InterimaireId");

                    b.ToTable("Certifications");
                });

            modelBuilder.Entity("AleniaAPI.Models.Evaluation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AuteurId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CibleId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Commentaire")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("MissionId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Note")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AuteurId");

                    b.HasIndex("CibleId");

                    b.HasIndex("MissionId");

                    b.ToTable("Evaluations");
                });

            modelBuilder.Entity("AleniaAPI.Models.Mission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<Guid>("EtablissementId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Horaires")
                        .HasColumnType("longtext");

                    b.Property<string>("Poste")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("TauxHoraire")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EtablissementId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("AleniaAPI.Models.Utilisateur", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MotDePassHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.HasKey("Id");

                    b.ToTable("Utilisateurs");

                    b.HasDiscriminator<string>("Role").HasValue("Utilisateur");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AleniaAPI.Models.Admin", b =>
                {
                    b.HasBaseType("AleniaAPI.Models.Utilisateur");

                    b.Property<string>("Pseudo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("admin");
                });

            modelBuilder.Entity("AleniaAPI.Models.Etablissement", b =>
                {
                    b.HasBaseType("AleniaAPI.Models.Utilisateur");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telephone")
                        .HasColumnType("longtext");

                    b.Property<string>("TypeEtablissement")
                        .HasColumnType("longtext");

                    b.ToTable("Utilisateurs", t =>
                        {
                            t.Property("Adresse")
                                .HasColumnName("Etablissement_Adresse");

                            t.Property("Nom")
                                .HasColumnName("Etablissement_Nom");

                            t.Property("Telephone")
                                .HasColumnName("Etablissement_Telephone");
                        });

                    b.HasDiscriminator().HasValue("etablissement");
                });

            modelBuilder.Entity("AleniaAPI.Models.Interimaire", b =>
                {
                    b.HasBaseType("AleniaAPI.Models.Utilisateur");

                    b.Property<string>("Adresse")
                        .HasColumnType("longtext");

                    b.Property<string>("Competences")
                        .HasColumnType("longtext");

                    b.Property<string>("Disponibilites")
                        .HasColumnType("longtext");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Telephone")
                        .HasColumnType("longtext");

                    b.HasDiscriminator().HasValue("interimaire");
                });

            modelBuilder.Entity("AleniaAPI.Models.Candidature", b =>
                {
                    b.HasOne("AleniaAPI.Models.Interimaire", "Interimaire")
                        .WithMany("Candidatures")
                        .HasForeignKey("InterimaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AleniaAPI.Models.Mission", "Mission")
                        .WithMany("Candidatures")
                        .HasForeignKey("MissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interimaire");

                    b.Navigation("Mission");
                });

            modelBuilder.Entity("AleniaAPI.Models.Certification", b =>
                {
                    b.HasOne("AleniaAPI.Models.Interimaire", "Interimaire")
                        .WithMany("Certifications")
                        .HasForeignKey("InterimaireId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interimaire");
                });

            modelBuilder.Entity("AleniaAPI.Models.Evaluation", b =>
                {
                    b.HasOne("AleniaAPI.Models.Utilisateur", "Auteur")
                        .WithMany()
                        .HasForeignKey("AuteurId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AleniaAPI.Models.Utilisateur", "Cible")
                        .WithMany()
                        .HasForeignKey("CibleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AleniaAPI.Models.Mission", "Mission")
                        .WithMany()
                        .HasForeignKey("MissionId");

                    b.Navigation("Auteur");

                    b.Navigation("Cible");

                    b.Navigation("Mission");
                });

            modelBuilder.Entity("AleniaAPI.Models.Mission", b =>
                {
                    b.HasOne("AleniaAPI.Models.Etablissement", "Etablissement")
                        .WithMany("Missions")
                        .HasForeignKey("EtablissementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etablissement");
                });

            modelBuilder.Entity("AleniaAPI.Models.Mission", b =>
                {
                    b.Navigation("Candidatures");
                });

            modelBuilder.Entity("AleniaAPI.Models.Etablissement", b =>
                {
                    b.Navigation("Missions");
                });

            modelBuilder.Entity("AleniaAPI.Models.Interimaire", b =>
                {
                    b.Navigation("Candidatures");

                    b.Navigation("Certifications");
                });
#pragma warning restore 612, 618
        }
    }
}
