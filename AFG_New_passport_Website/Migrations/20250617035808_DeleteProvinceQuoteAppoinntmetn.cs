using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class DeleteProvinceQuoteAppoinntmetn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProvinceQuotas_ProvinceQuotaId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProvinceQuotaId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ProvinceQuotaId",
                table: "Appointments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProvinceQuotaId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProvinceQuotaId",
                table: "Appointments",
                column: "ProvinceQuotaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ProvinceQuotas_ProvinceQuotaId",
                table: "Appointments",
                column: "ProvinceQuotaId",
                principalTable: "ProvinceQuotas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
