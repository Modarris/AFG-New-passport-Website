using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AFG_New_passport_Website.Migrations
{
    public partial class BankAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPDAccount",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "RedcrossFee",
                table: "BankAccounts",
                newName: "UniteCode");

            migrationBuilder.RenameColumn(
                name: "RedcrossAccount",
                table: "BankAccounts",
                newName: "CamercialBankAccount");

            migrationBuilder.RenameColumn(
                name: "PostFee",
                table: "BankAccounts",
                newName: "PassportRevenueCode");

            migrationBuilder.RenameColumn(
                name: "PostAccount",
                table: "BankAccounts",
                newName: "AccountNumber");

            migrationBuilder.AddColumn<string>(
                name: "AccountHolder",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fee",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationCode",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrgCode",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PassportBookletRevenueCode",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountHolder",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Fee",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "LocationCode",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "OrgCode",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "PassportBookletRevenueCode",
                table: "BankAccounts");

            migrationBuilder.RenameColumn(
                name: "UniteCode",
                table: "BankAccounts",
                newName: "RedcrossFee");

            migrationBuilder.RenameColumn(
                name: "PassportRevenueCode",
                table: "BankAccounts",
                newName: "PostFee");

            migrationBuilder.RenameColumn(
                name: "CamercialBankAccount",
                table: "BankAccounts",
                newName: "RedcrossAccount");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "BankAccounts",
                newName: "PostAccount");

            migrationBuilder.AddColumn<string>(
                name: "CPDAccount",
                table: "BankAccounts",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");
        }
    }
}
