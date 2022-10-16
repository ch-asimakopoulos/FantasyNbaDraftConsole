using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyNbaDraftConsole.Migrations
{
    public partial class letsdraft64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "PickNumber",
                schema: "public",
                table: "Players",
                type: "smallint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickNumber",
                schema: "public",
                table: "Players");
        }
    }
}
