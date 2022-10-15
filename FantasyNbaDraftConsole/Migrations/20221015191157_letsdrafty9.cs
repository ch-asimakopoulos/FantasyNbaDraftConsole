using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdrafty9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Players_PlayerId1",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_PlayerId1",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "Positions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId1",
                table: "Positions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PlayerId1",
                table: "Positions",
                column: "PlayerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Players_PlayerId1",
                table: "Positions",
                column: "PlayerId1",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }
    }
}
