using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addednewcolumnintheFranchiseRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "franchise_request_report",
                table: "FranchiseRequestModel");

            migrationBuilder.AddColumn<bool>(
                name: "franchise_customized_option",
                table: "FranchiseRequestModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "franchise_customized_option_id",
                table: "FranchiseRequestModel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "franchise_customized_option",
                table: "FranchiseRequestModel");

            migrationBuilder.DropColumn(
                name: "franchise_customized_option_id",
                table: "FranchiseRequestModel");

            migrationBuilder.AddColumn<string>(
                name: "franchise_request_report",
                table: "FranchiseRequestModel",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
