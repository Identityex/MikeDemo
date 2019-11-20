using Microsoft.EntityFrameworkCore.Migrations;

namespace MikeDemoDBEntities.Migrations
{
    public partial class AddData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AutomobileTypes",
                columns: new[] { "AutomobileTypeId", "description", "type", "wheels" },
                values: new object[] { 1, "A Truck", "Truck", 4 });

            migrationBuilder.InsertData(
                table: "AutomobileTypes",
                columns: new[] { "AutomobileTypeId", "description", "type", "wheels" },
                values: new object[] { 2, "A Car", "Car", 4 });

            migrationBuilder.InsertData(
                table: "AutomobileTypes",
                columns: new[] { "AutomobileTypeId", "description", "type", "wheels" },
                values: new object[] { 3, "A Motorbike", "MotorBike", 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AutomobileTypes",
                keyColumn: "AutomobileTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AutomobileTypes",
                keyColumn: "AutomobileTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AutomobileTypes",
                keyColumn: "AutomobileTypeId",
                keyValue: 3);
        }
    }
}
