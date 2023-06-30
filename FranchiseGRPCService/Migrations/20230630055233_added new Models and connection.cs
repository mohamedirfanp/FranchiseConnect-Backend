using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addednewModelsandconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FranchiseCustomizedOptionModel",
                columns: table => new
                {
                    franchise_customized_option_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_customized_option_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseCustomizedOptionModel", x => x.franchise_customized_option_id);
                });

            migrationBuilder.CreateTable(
                name: "FranchiseSelectedService",
                columns: table => new
                {
                    franchise_selected_service_id = table.Column<int>(type: "int", nullable: false),
                    franchise_provide_service_id = table.Column<int>(type: "int", nullable: false),
                    franchise_customized_option_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseSelectedService", x => x.franchise_selected_service_id);
                    table.ForeignKey(
                        name: "FK_FranchiseSelectedService_FranchiseCustomizedOptionModel_franchise_selected_service_id",
                        column: x => x.franchise_selected_service_id,
                        principalTable: "FranchiseCustomizedOptionModel",
                        principalColumn: "franchise_customized_option_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FranchiseSelectedService_FranchiseService_franchise_provide_service_id",
                        column: x => x.franchise_provide_service_id,
                        principalSchema: "dbo",
                        principalTable: "FranchiseService",
                        principalColumn: "franchise_service_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseSelectedService_franchise_provide_service_id",
                table: "FranchiseSelectedService",
                column: "franchise_provide_service_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FranchiseSelectedService");

            migrationBuilder.DropTable(
                name: "FranchiseCustomizedOptionModel");
        }
    }
}
