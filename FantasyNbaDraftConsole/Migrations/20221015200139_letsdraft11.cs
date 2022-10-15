using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdraft11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:CollationDefinition:nba_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .Annotation("Npgsql:Enum:position_type_id", "invalid,point_guard,shooting_guard,small_forward,power_forward,center,guard,forward")
                .Annotation("Npgsql:Enum:projection_totals_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game,total_ranking")
                .Annotation("Npgsql:Enum:projection_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage")
                .OldAnnotation("Npgsql:Enum:position_type_id", "invalid,point_guard,shooting_guard,small_forward,power_forward,center,guard,forward")
                .OldAnnotation("Npgsql:Enum:projection_totals_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game,total_ranking")
                .OldAnnotation("Npgsql:Enum:projection_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage");

            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Teams",
                type: "text",
                nullable: true,
                collation: "nba_collation",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NbaTeam",
                table: "Players",
                type: "text",
                nullable: true,
                collation: "nba_collation",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "text",
                nullable: true,
                collation: "nba_collation",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "Leagues",
                type: "text",
                nullable: true,
                collation: "nba_collation",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:position_type_id", "invalid,point_guard,shooting_guard,small_forward,power_forward,center,guard,forward")
                .Annotation("Npgsql:Enum:projection_totals_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game,total_ranking")
                .Annotation("Npgsql:Enum:projection_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage")
                .OldAnnotation("Npgsql:CollationDefinition:nba_collation", "en-u-ks-primary,en-u-ks-primary,icu,False")
                .OldAnnotation("Npgsql:Enum:position_type_id", "invalid,point_guard,shooting_guard,small_forward,power_forward,center,guard,forward")
                .OldAnnotation("Npgsql:Enum:projection_totals_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goals_made,field_goals_attempted,three_pointers_made,three_pointers_attempted,free_throws_made,free_throws_attempted,games_played,minutes_per_game,total_ranking")
                .OldAnnotation("Npgsql:Enum:projection_type_id", "invalid,points,assists,rebounds,blocks,steals,turnovers,field_goal_percentage,three_pointers_made,free_throw_percentage");

            migrationBuilder.AlterColumn<string>(
                name: "TeamName",
                table: "Teams",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "nba_collation");

            migrationBuilder.AlterColumn<string>(
                name: "NbaTeam",
                table: "Players",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "nba_collation");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "nba_collation");

            migrationBuilder.AlterColumn<string>(
                name: "LeagueName",
                table: "Leagues",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldCollation: "nba_collation");
        }
    }
}
