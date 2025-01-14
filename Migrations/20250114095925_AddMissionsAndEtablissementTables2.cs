using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AleniaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddMissionsAndEtablissementTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EtablissementId",
                table: "Missions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EtablissementId",
                table: "Missions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
