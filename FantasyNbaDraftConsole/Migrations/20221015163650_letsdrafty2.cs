using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdrafty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectedAssistScore",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedBlockScore",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedFieldGoalScore",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedFreeThrowScore",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedGames",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedMinutes",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedPointScoe",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedReboundScore",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedStealScore",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedThreePointMadeScore",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ProjectedTurnoverScore",
                table: "Players");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:projection_totals_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game")
                .Annotation("Npgsql:Enum:projection_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage");

            migrationBuilder.AddColumn<bool>(
                name: "Starred",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Projection",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    ProjectionTypeId = table.Column<byte>(type: "smallint", nullable: false),
                    ProjectionValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projection", x => new { x.PlayerId, x.ProjectionTypeId });
                    table.ForeignKey(
                        name: "FK_Projection_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectionTotal",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    ProjectionTotalsTypeId = table.Column<byte>(type: "smallint", nullable: false),
                    ProjectionValue = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectionTotal", x => new { x.PlayerId, x.ProjectionTotalsTypeId });
                    table.ForeignKey(
                        name: "FK_ProjectionTotal_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "PlayerId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projection");

            migrationBuilder.DropTable(
                name: "ProjectionTotal");

            migrationBuilder.DropColumn(
                name: "Starred",
                table: "Players");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:projection_totals_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game")
                .OldAnnotation("Npgsql:Enum:projection_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage");

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedAssistScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedBlockScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedFieldGoalScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedFreeThrowScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ProjectedGames",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedMinutes",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedPointScoe",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedReboundScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedStealScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedThreePointMadeScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "ProjectedTurnoverScore",
                table: "Players",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
