﻿// <auto-generated />
using System;
using AleniaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AleniaAPI.Migrations
{
    [DbContext(typeof(AleniaContext))]
    [Migration("20250114095426_AddMissionsAndEtablissementTables")]
    partial class AddMissionsAndEtablissementTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("AleniaAPI.Models.Etablissements", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Etablissements");
                });

            modelBuilder.Entity("AleniaAPI.Models.Missions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EtablissementId")
                        .HasColumnType("int");

                    b.Property<Guid>("EtablissementsId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Poste")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("TauxHoraire")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("EtablissementsId");

                    b.ToTable("Missions");
                });

            modelBuilder.Entity("AleniaAPI.Models.Missions", b =>
                {
                    b.HasOne("AleniaAPI.Models.Etablissements", "Etablissements")
                        .WithMany("Missions")
                        .HasForeignKey("EtablissementsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Etablissements");
                });

            modelBuilder.Entity("AleniaAPI.Models.Etablissements", b =>
                {
                    b.Navigation("Missions");
                });
#pragma warning restore 612, 618
        }
    }
}
