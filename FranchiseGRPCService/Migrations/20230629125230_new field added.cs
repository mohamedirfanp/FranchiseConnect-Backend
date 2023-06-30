using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class newfieldadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "franchise_provider_service_name",
                schema: "dbo",
                table: "FranchiseService",
                newName: "franchise_provided_service_name");

            migrationBuilder.AddColumn<string>(
                name: "franchise_provided_service_description",
                schema: "dbo",
                table: "FranchiseService",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "franchise_provided_service_description",
                schema: "dbo",
                table: "FranchiseService");

            migrationBuilder.RenameColumn(
                name: "franchise_provided_service_name",
                schema: "dbo",
                table: "FranchiseService",
                newName: "franchise_provider_service_name");
        }
    }
}
