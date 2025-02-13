using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AleniaAPI.Migrations
{
    /// <inheritdoc />
    public partial class MissionModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePublication",
                table: "Missions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Poste",
                table: "Missions",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "TauxHoraire",
                table: "Missions",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatePublication",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "Poste",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "TauxHoraire",
                table: "Missions");
        }
    }
}
