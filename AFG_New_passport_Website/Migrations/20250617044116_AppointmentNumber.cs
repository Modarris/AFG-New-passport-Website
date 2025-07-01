using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class AppointmentNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentNumber",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentNumber",
                table: "Appointments");
        }
    }
}
