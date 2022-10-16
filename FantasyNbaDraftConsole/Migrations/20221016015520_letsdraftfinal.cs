using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdraftfinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_Name",
                schema: "public",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_Name",
                schema: "public",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_DraftPosition",
                schema: "public",
                table: "Teams",
                column: "DraftPosition",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                schema: "public",
                table: "Teams",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                schema: "public",
                table: "Players",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_PickNumber",
                schema: "public",
                table: "Players",
                column: "PickNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_DraftPosition",
                schema: "public",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_Name",
                schema: "public",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Players_Name",
                schema: "public",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_PickNumber",
                schema: "public",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                schema: "public",
                table: "Teams",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                schema: "public",
                table: "Players",
                column: "Name");
        }
    }
}
