using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AleniaAPI.Migrations
{
    /// <inheritdoc />
    public partial class TestForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EtablissementId",
                table: "Missions",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "Etablissements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etablissements", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_EtablissementId",
                table: "Missions",
                column: "EtablissementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Etablissements_EtablissementId",
                table: "Missions",
                column: "EtablissementId",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Etablissements_EtablissementId",
                table: "Missions");

            migrationBuilder.DropTable(
                name: "Etablissements");

            migrationBuilder.DropIndex(
                name: "IX_Missions_EtablissementId",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "EtablissementId",
                table: "Missions");
        }
    }
}
