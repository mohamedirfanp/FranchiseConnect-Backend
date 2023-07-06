using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class removecustomizedmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseCustomizedOptionModel_franchise_selected_service_id",
                table: "FranchiseSelectedServiceModel");

            migrationBuilder.DropTable(
                name: "FranchiseCustomizedOptionModel");

            migrationBuilder.DropColumn(
                name: "franchise_customized_option_id",
                table: "FranchiseSelectedServiceModel");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "franchise_selected_service_id",
                table: "FranchiseSelectedServiceModel",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "franchise_customized_option_id",
                table: "FranchiseSelectedServiceModel",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_FranchiseSelectedServiceModel_FranchiseCustomizedOptionModel_franchise_selected_service_id",
                table: "FranchiseSelectedServiceModel",
                column: "franchise_selected_service_id",
                principalTable: "FranchiseCustomizedOptionModel",
                principalColumn: "franchise_customized_option_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
