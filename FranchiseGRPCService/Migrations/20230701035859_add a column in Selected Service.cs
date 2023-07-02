using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addacolumninSelectedService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedService_FranchiseCustomizedOptionModel_franchise_selected_service_id",
                table: "FranchiseSelectedService");

            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedService_FranchiseService_franchise_provide_service_id",
                table: "FranchiseSelectedService");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FranchiseSelectedService",
                table: "FranchiseSelectedService");

            migrationBuilder.RenameTable(
                name: "FranchiseSelectedService",
                newName: "FranchiseSelectedServiceModel");

            migrationBuilder.RenameIndex(
                name: "IX_FranchiseSelectedService_franchise_provide_service_id",
                table: "FranchiseSelectedServiceModel",
                newName: "IX_FranchiseSelectedServiceModel_franchise_provide_service_id");

            migrationBuilder.AddColumn<bool>(
                name: "user_customization",
                table: "FranchiseSelectedServiceModel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FranchiseSelectedServiceModel",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_selected_service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseCustomizedOptionModel_franchise_selected_service_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_selected_service_id",
                principalTable: "FranchiseCustomizedOptionModel",
                principalColumn: "franchise_customized_option_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseService_franchise_provide_service_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_provide_service_id",
                principalSchema: "dbo",
                principalTable: "FranchiseService",
                principalColumn: "franchise_service_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseCustomizedOptionModel_franchise_selected_service_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseService_franchise_provide_service_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FranchiseSelectedServiceModel",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropColumn(
                name: "user_customization",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.RenameTable(
                name: "FranchiseSelectedServiceModel",
                newName: "FranchiseSelectedService");

            migrationBuilder.RenameIndex(
                name: "IX_FranchiseSelectedServiceModel_franchise_provide_service_id",
                table: "FranchiseSelectedService",
                newName: "IX_FranchiseSelectedService_franchise_provide_service_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FranchiseSelectedService",
                table: "FranchiseSelectedService",
                column: "franchise_selected_service_id");

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedService_FranchiseCustomizedOptionModel_franchise_selected_service_id",
                table: "FranchiseSelectedService",
                column: "franchise_selected_service_id",
                principalTable: "FranchiseCustomizedOptionModel",
                principalColumn: "franchise_customized_option_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedService_FranchiseService_franchise_provide_service_id",
                table: "FranchiseSelectedService",
                column: "franchise_provide_service_id",
                principalSchema: "dbo",
                principalTable: "FranchiseService",
                principalColumn: "franchise_service_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
