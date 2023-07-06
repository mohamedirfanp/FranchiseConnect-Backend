using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class requestmodelchangedtoone2many : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FranchiseRequestModel_franchise_id",
                table: "FranchiseRequestModel");

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseRequestModel_franchise_id",
                table: "FranchiseRequestModel",
                column: "franchise_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FranchiseRequestModel_franchise_id",
                table: "FranchiseRequestModel");

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseRequestModel_franchise_id",
                table: "FranchiseRequestModel",
                column: "franchise_id",
                unique: true);
        }
    }
}
