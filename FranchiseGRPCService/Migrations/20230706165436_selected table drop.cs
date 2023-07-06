using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class selectedtabledrop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseRequestModel_franchise_request_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FranchiseSelectedServiceModel",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.RenameTable(
                name: "FranchiseSelectedServiceModel",
                newName: "FranchiseSelectedService");

            migrationBuilder.RenameIndex(
                name: "IX_FranchiseSelectedServiceModel_franchise_request_id",
                table: "FranchiseSelectedService",
                newName: "IX_FranchiseSelectedService_franchise_request_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FranchiseSelectedService",
                table: "FranchiseSelectedService",
                column: "franchise_selected_service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedService_FranchiseRequestModel_franchise_request_id",
                table: "FranchiseSelectedService",
                column: "franchise_request_id",
                principalTable: "FranchiseRequestModel",
                principalColumn: "franchise_request_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedService_FranchiseRequestModel_franchise_request_id",
                table: "FranchiseSelectedService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FranchiseSelectedService",
                table: "FranchiseSelectedService");

            migrationBuilder.RenameTable(
                name: "FranchiseSelectedService",
                newName: "FranchiseSelectedServiceModel");

            migrationBuilder.RenameIndex(
                name: "IX_FranchiseSelectedService_franchise_request_id",
                table: "FranchiseSelectedServiceModel",
                newName: "IX_FranchiseSelectedServiceModel_franchise_request_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FranchiseSelectedServiceModel",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_selected_service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseRequestModel_franchise_request_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_request_id",
                principalTable: "FranchiseRequestModel",
                principalColumn: "franchise_request_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
