using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addremoveusercustomization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_customization",
                table: "FranchiseSelectedServiceModel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "user_customization",
                table: "FranchiseSelectedServiceModel",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
