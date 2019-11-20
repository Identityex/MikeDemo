using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeDemoDBEntities.Migrations
{
    public partial class InitializeDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutomobileTypes",
                columns: table => new
                {
                    AutomobileTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    wheels = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomobileTypes", x => x.AutomobileTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Automobiles",
                columns: table => new
                {
                    AutomobileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    JsonDetails = table.Column<string>(nullable: true),
                    AutomobileTypesAutomobileTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Automobiles", x => x.AutomobileId);
                    table.ForeignKey(
                        name: "FK_Automobiles_AutomobileTypes_AutomobileTypesAutomobileTypeId",
                        column: x => x.AutomobileTypesAutomobileTypeId,
                        principalTable: "AutomobileTypes",
                        principalColumn: "AutomobileTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Automobiles_AutomobileTypesAutomobileTypeId",
                table: "Automobiles",
                column: "AutomobileTypesAutomobileTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Automobiles");

            migrationBuilder.DropTable(
                name: "AutomobileTypes");
        }
    }
}
