using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdraft44 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectionTotal_Players_PlayerId",
                table: "ProjectionTotal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectionTotal",
                table: "ProjectionTotal");

            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Teams",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Projection",
                newName: "Projection",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Positions",
                newName: "Positions",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Players",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "Leagues",
                newName: "Leagues",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "DraftConfig",
                newName: "DraftConfig",
                newSchema: "public");

            migrationBuilder.RenameTable(
                name: "ProjectionTotal",
                newName: "ProjectionTotals",
                newSchema: "public");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectionTotals",
                schema: "public",
                table: "ProjectionTotals",
                columns: new[] { "PlayerId", "ProjectionTotalsTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectionTotals_Players_PlayerId",
                schema: "public",
                table: "ProjectionTotals",
                column: "PlayerId",
                principalSchema: "public",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectionTotals_Players_PlayerId",
                schema: "public",
                table: "ProjectionTotals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectionTotals",
                schema: "public",
                table: "ProjectionTotals");

            migrationBuilder.RenameTable(
                name: "Teams",
                schema: "public",
                newName: "Teams");

            migrationBuilder.RenameTable(
                name: "Projection",
                schema: "public",
                newName: "Projection");

            migrationBuilder.RenameTable(
                name: "Positions",
                schema: "public",
                newName: "Positions");

            migrationBuilder.RenameTable(
                name: "Players",
                schema: "public",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Leagues",
                schema: "public",
                newName: "Leagues");

            migrationBuilder.RenameTable(
                name: "DraftConfig",
                schema: "public",
                newName: "DraftConfig");

            migrationBuilder.RenameTable(
                name: "ProjectionTotals",
                schema: "public",
                newName: "ProjectionTotal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectionTotal",
                table: "ProjectionTotal",
                columns: new[] { "PlayerId", "ProjectionTotalsTypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectionTotal_Players_PlayerId",
                table: "ProjectionTotal",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "PlayerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
