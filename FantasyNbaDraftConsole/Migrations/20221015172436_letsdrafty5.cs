using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdrafty5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueDraftRounds",
                table: "Leagues");

            migrationBuilder.CreateTable(
                name: "DraftConfig",
                columns: table => new
                {
                    LeagueId = table.Column<byte>(type: "smallint", nullable: false),
                    Snake = table.Column<bool>(type: "boolean", nullable: false),
                    Rounds = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraftConfig", x => x.LeagueId);
                    table.ForeignKey(
                        name: "FK_DraftConfig_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraftConfig");

            migrationBuilder.AddColumn<byte>(
                name: "LeagueDraftRounds",
                table: "Leagues",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
