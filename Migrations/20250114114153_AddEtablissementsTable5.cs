using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AleniaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEtablissementsTable5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Missions_Etablissements_EtablissementsId1",
                table: "Missions");

            migrationBuilder.DropIndex(
                name: "IX_Missions_EtablissementsId1",
                table: "Missions");

            migrationBuilder.DropColumn(
                name: "EtablissementsId1",
                table: "Missions");

            migrationBuilder.AlterColumn<int>(
                name: "EtablissementsId",
                table: "Missions",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

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

            migrationBuilder.DropIndex(
                name: "IX_Missions_EtablissementsId",
                table: "Missions");

            migrationBuilder.AlterColumn<Guid>(
                name: "EtablissementsId",
                table: "Missions",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EtablissementsId1",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_EtablissementsId1",
                table: "Missions",
                column: "EtablissementsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Missions_Etablissements_EtablissementsId1",
                table: "Missions",
                column: "EtablissementsId1",
                principalTable: "Etablissements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
