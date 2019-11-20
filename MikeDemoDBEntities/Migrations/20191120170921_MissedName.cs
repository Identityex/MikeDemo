using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeDemoDBEntities.Migrations
{
    public partial class MissedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Automobiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Automobiles");
        }
    }
}
