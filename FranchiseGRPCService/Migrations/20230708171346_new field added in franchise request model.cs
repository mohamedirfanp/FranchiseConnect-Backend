using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class newfieldaddedinfranchiserequestmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "investment_budget",
                table: "FranchiseRequestModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "space",
                table: "FranchiseRequestModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "investment_budget",
                table: "FranchiseRequestModel");

            migrationBuilder.DropColumn(
                name: "space",
                table: "FranchiseRequestModel");
        }
    }
}
