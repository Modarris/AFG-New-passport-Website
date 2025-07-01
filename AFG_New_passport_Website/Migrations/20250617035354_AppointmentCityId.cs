using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class AppointmentCityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Cities_CityId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Appointments",
                newName: "ProcessingCityId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CityId",
                table: "Appointments",
                newName: "IX_Appointments_ProcessingCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Cities_ProcessingCityId",
                table: "Appointments",
                column: "ProcessingCityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Cities_ProcessingCityId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "ProcessingCityId",
                table: "Appointments",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_ProcessingCityId",
                table: "Appointments",
                newName: "IX_Appointments_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Cities_CityId",
                table: "Appointments",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
