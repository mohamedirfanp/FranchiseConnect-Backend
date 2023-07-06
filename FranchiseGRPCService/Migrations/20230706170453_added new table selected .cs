using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addednewtableselected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "franchiseSelectedServiceModels",
                columns: table => new
                {
                    franchise_selected_service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_provide_service_id = table.Column<int>(type: "int", nullable: false),
                    franchise_request_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_franchiseSelectedServiceModels", x => x.franchise_selected_service_id);
                    table.ForeignKey(
                        name: "FK_franchiseSelectedServiceModels_FranchiseRequestModel_franchise_request_id",
                        column: x => x.franchise_request_id,
                        principalTable: "FranchiseRequestModel",
                        principalColumn: "franchise_request_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_franchiseSelectedServiceModels_franchise_request_id",
                table: "franchiseSelectedServiceModels",
                column: "franchise_request_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "franchiseSelectedServiceModels");

            migrationBuilder.CreateTable(
                name: "FranchiseSelectedService",
                columns: table => new
                {
                    franchise_selected_service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_request_id = table.Column<int>(type: "int", nullable: false),
                    franchise_provide_service_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseSelectedService", x => x.franchise_selected_service_id);
                    table.ForeignKey(
                        name: "FK_FranchiseSelectedService_FranchiseRequestModel_franchise_request_id",
                        column: x => x.franchise_request_id,
                        principalTable: "FranchiseRequestModel",
                        principalColumn: "franchise_request_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseSelectedService_franchise_request_id",
                table: "FranchiseSelectedService",
                column: "franchise_request_id");
        }
    }
}
