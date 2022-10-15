using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdrafty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueId = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    LeagueName = table.Column<string>(type: "text", nullable: true),
                    LeagueDraftRounds = table.Column<byte>(type: "smallint", nullable: false),
                    LeagueInitialized = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    LeagueId = table.Column<byte>(type: "smallint", nullable: false),
                    TeamName = table.Column<string>(type: "text", nullable: true),
                    TeamDraftPosition = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TeamId = table.Column<byte>(type: "smallint", nullable: true),
                    Injured = table.Column<bool>(type: "boolean", nullable: false),
                    NbaTeam = table.Column<string>(type: "text", nullable: true),
                    ProjectedGames = table.Column<int>(type: "integer", nullable: false),
                    ProjectedMinutes = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedPointScoe = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedBlockScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedStealScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedAssistScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedReboundScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedTurnoverScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedFieldGoalScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedFreeThrowScore = table.Column<decimal>(type: "numeric", nullable: false),
                    ProjectedThreePointMadeScore = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerId);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId");
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    PositionName = table.Column<string>(type: "text", nullable: true),
                    PlayerId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                    table.ForeignKey(
                        name: "FK_Positions_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PlayerId",
                table: "Positions",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LeagueId",
                table: "Teams",
                column: "LeagueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Leagues");
        }
    }
}
