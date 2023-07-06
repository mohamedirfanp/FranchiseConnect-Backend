using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addrequestmodelfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseService_franchise_provide_service_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_FranchiseSelectedServiceModel_franchise_provide_service_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropColumn(
                name: "franchise_customized_option_id",
                table: "FranchiseRequestModel");

            migrationBuilder.AddColumn<int>(
                name: "franchise_request_id",
                table: "FranchiseSelectedServiceModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseSelectedServiceModel_franchise_request_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_request_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseRequestModel_franchise_request_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_request_id",
                principalTable: "FranchiseRequestModel",
                principalColumn: "franchise_request_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseRequestModel_franchise_request_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropIndex(
                name: "IX_FranchiseSelectedServiceModel_franchise_request_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropColumn(
                name: "franchise_request_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.AddColumn<int>(
                name: "franchise_customized_option_id",
                table: "FranchiseRequestModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseSelectedServiceModel_franchise_provide_service_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_provide_service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseService_franchise_provide_service_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_provide_service_id",
                principalSchema: "dbo",
                principalTable: "FranchiseService",
                principalColumn: "franchise_service_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
