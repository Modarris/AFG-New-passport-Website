using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class newProvinceQuote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas");

            migrationBuilder.DropColumn(
                name: "ProcessingCityId",
                table: "ProvinceQuotas");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "ProvinceQuotas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "ProvinceQuotas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessingCityId",
                table: "ProvinceQuotas",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
