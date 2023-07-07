using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addednewfieldinselectedService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_franchiseSelectedServiceModels_franchise_provide_service_id",
                table: "franchiseSelectedServiceModels",
                column: "franchise_provide_service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_franchiseSelectedServiceModels_FranchiseService_franchise_provide_service_id",
                table: "franchiseSelectedServiceModels",
                column: "franchise_provide_service_id",
                principalSchema: "dbo",
                principalTable: "FranchiseService",
                principalColumn: "franchise_service_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_franchiseSelectedServiceModels_FranchiseService_franchise_provide_service_id",
                table: "franchiseSelectedServiceModels");

            migrationBuilder.DropIndex(
                name: "IX_franchiseSelectedServiceModels_franchise_provide_service_id",
                table: "franchiseSelectedServiceModels");
        }
    }
}
