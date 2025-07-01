using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class ProvinceQuoteCityId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "ProvinceQuotas",
                newName: "ProcessingCityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProvinceQuotas_CityId",
                table: "ProvinceQuotas",
                newName: "IX_ProvinceQuotas_ProcessingCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceQuotas_Cities_ProcessingCityId",
                table: "ProvinceQuotas",
                column: "ProcessingCityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceQuotas_Cities_ProcessingCityId",
                table: "ProvinceQuotas");

            migrationBuilder.RenameColumn(
                name: "ProcessingCityId",
                table: "ProvinceQuotas",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProvinceQuotas_ProcessingCityId",
                table: "ProvinceQuotas",
                newName: "IX_ProvinceQuotas_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
