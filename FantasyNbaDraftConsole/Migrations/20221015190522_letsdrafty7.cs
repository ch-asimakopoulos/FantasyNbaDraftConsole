using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdrafty7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Players_PlayerId",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_PlayerId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "PositionName",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "PositionId",
                table: "Positions",
                newName: "PositionTypeId");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:position_type_id", "invalid,point_guard,shooting_guard,small_forward,power_forward,center,guard,forward")
                .Annotation("Npgsql:Enum:projection_totals_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game,total_ranking")
                .Annotation("Npgsql:Enum:projection_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage")
                .OldAnnotation("Npgsql:Enum:projection_totals_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game")
                .OldAnnotation("Npgsql:Enum:projection_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Positions",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "PositionTypeId",
                table: "Positions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AddColumn<int>(
                name: "PlayerId1",
                table: "Positions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                columns: new[] { "PlayerId", "PositionTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PlayerId1",
                table: "Positions",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name",
                table: "Players",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Players_PlayerId",
                table: "Positions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Players_PlayerId1",
                table: "Positions",
                column: "PlayerId1",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Players_PlayerId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Players_PlayerId1",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Positions",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_PlayerId1",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Players_Name",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerId1",
                table: "Positions");

            migrationBuilder.RenameColumn(
                name: "PositionTypeId",
                table: "Positions",
                newName: "PositionId");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:projection_totals_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game")
                .Annotation("Npgsql:Enum:projection_type", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage")
                .OldAnnotation("Npgsql:Enum:position_type_id", "invalid,point_guard,shooting_guard,small_forward,power_forward,center,guard,forward")
                .OldAnnotation("Npgsql:Enum:projection_totals_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game,total_ranking")
                .OldAnnotation("Npgsql:Enum:projection_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage");

            migrationBuilder.AlterColumn<int>(
                name: "PlayerId",
                table: "Positions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<byte>(
                name: "PositionId",
                table: "Positions",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.AddColumn<string>(
                name: "PositionName",
                table: "Positions",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Positions",
                table: "Positions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PlayerId",
                table: "Positions",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Players_PlayerId",
                table: "Positions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }
    }
}
