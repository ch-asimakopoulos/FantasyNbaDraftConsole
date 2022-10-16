using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdraft664 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeamName",
                schema: "public",
                table: "Teams",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TeamDraftPosition",
                schema: "public",
                table: "Teams",
                newName: "DraftPosition");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                schema: "public",
                table: "Teams",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_Name",
                schema: "public",
                table: "Teams");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "public",
                table: "Teams",
                newName: "TeamName");

            migrationBuilder.RenameColumn(
                name: "DraftPosition",
                schema: "public",
                table: "Teams",
                newName: "TeamDraftPosition");
        }
    }
}
