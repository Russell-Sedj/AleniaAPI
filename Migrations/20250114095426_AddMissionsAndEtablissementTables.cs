using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AleniaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMissionsAndEtablissementTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Missions",
                newName: "Poste");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Missions",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDebut",
                table: "Missions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFin",
                table: "Missions",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EtablissementId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "EtablissementsId",
                table: "Missions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<decimal>(
                name: "TauxHoraire",
                table: "Missions",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Etablissements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nom = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etablissements", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_EtablissementsId",
                table: "Missions",
                column: "EtablissementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Etablissements_EtablissementsId",
                table: "Missions",
                column: "EtablissementsId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Etablissements_EtablissementsId",
                table: "Missions");

            migrationBuilder.DropTable(
                name: "Etablissements");

            migrationBuilder.DropIndex(
                name: "IX_Missions_EtablissementsId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "DateDebut",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "DateFin",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "EtablissementId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "EtablissementsId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "TauxHoraire",
                table: "Missions");

            migrationBuilder.RenameColumn(
                name: "Poste",
                table: "Missions",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Missions",
                newName: "Name");
        }
    }
}
