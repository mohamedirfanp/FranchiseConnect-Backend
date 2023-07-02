using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWishlistModel",
                columns: table => new
                {
                    UserWishlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FranchiseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWishlistModel", x => x.UserWishlistId);
                    table.ForeignKey(
                        name: "FK_UserWishlistModel_Franchise_FranchiseId",
                        column: x => x.FranchiseId,
                        principalSchema: "dbo",
                        principalTable: "Franchise",
                        principalColumn: "franchise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWishlistModel_FranchiseId",
                table: "UserWishlistModel",
                column: "FranchiseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWishlistModel");
        }
    }
}
