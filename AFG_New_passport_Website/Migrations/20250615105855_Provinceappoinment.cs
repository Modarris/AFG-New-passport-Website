using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class Provinceappoinment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "EndTime",
                table: "ProvinceQuotas",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcessingCityId",
                table: "ProvinceQuotas",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "ProvinceQuotas");

            migrationBuilder.DropColumn(
                name: "ProcessingCityId",
                table: "ProvinceQuotas");
        }
    }
}
