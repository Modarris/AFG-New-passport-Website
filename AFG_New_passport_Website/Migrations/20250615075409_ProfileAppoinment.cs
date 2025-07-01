using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class ProfileAppoinment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProfileId",
                table: "Appointments",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Profiles_ProfileId",
                table: "Appointments",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Profile_Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Profiles_ProfileId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProfileId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Appointments");
        }
    }
}
