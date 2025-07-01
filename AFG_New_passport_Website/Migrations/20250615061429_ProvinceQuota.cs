using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class ProvinceQuota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceQuotas_Locations_LocationId",
                table: "ProvinceQuotas");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProvinceQuotas");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "ProvinceQuotas",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_ProvinceQuotas_LocationId",
                table: "ProvinceQuotas",
                newName: "IX_ProvinceQuotas_CityId");

            migrationBuilder.AlterColumn<string>(
                name: "RedCrossTarifaCode",
                table: "Tarifas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PostTarifaCode",
                table: "Tarifas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CPDTarifaCode",
                table: "Tarifas",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvinceQuotas_Cities_CityId",
                table: "ProvinceQuotas");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "ProvinceQuotas",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_ProvinceQuotas_CityId",
                table: "ProvinceQuotas",
                newName: "IX_ProvinceQuotas_LocationId");

            migrationBuilder.AlterColumn<string>(
                name: "RedCrossTarifaCode",
                table: "Tarifas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "PostTarifaCode",
                table: "Tarifas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "CPDTarifaCode",
                table: "Tarifas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProvinceQuotas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProvinceQuotas_Locations_LocationId",
                table: "ProvinceQuotas",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
