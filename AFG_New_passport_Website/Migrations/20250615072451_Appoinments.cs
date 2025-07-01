using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class Appoinments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Locations_LocationId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "AvailableSlots",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Appointments",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_LocationId",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Cities_CityId",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Appointments",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Appointments_CityId",
                table: "Appointments",
                newName: "IX_Appointments_LocationId");

            migrationBuilder.AddColumn<int>(
                name: "AvailableSlots",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Locations_LocationId",
                table: "Appointments",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
