using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class franchiseDBCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "FranchiseSocial",
                schema: "dbo",
                columns: table => new
                {
                    franchise_social_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_website = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    franchise_facebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    franchise_twitter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    franchise_instagram = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseSocial", x => x.franchise_social_id);
                });

            migrationBuilder.CreateTable(
                name: "Franchise",
                schema: "dbo",
                columns: table => new
                {
                    franchise_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_industry = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_segment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_about = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_investment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_fee = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_space = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_current_count = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_preferred_expansion_location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_view_count = table.Column<int>(type: "int", nullable: false),
                    franchise_sample_box_option = table.Column<bool>(type: "bit", nullable: false),
                    franchise_customized_option = table.Column<bool>(type: "bit", nullable: false),
                    franchise_customized_option_id = table.Column<int>(type: "int", nullable: false),
                    franchise_social_id = table.Column<int>(type: "int", nullable: false),
                    franchise_owner_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Franchise", x => x.franchise_id);
                    table.ForeignKey(
                        name: "FK_Franchise_FranchiseSocial_franchise_social_id",
                        column: x => x.franchise_social_id,
                        principalSchema: "dbo",
                        principalTable: "FranchiseSocial",
                        principalColumn: "franchise_social_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FranchiseGallery",
                schema: "dbo",
                columns: table => new
                {
                    franchise_gallery_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_photo_url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    franchise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseGallery", x => x.franchise_gallery_id);
                    table.ForeignKey(
                        name: "FK_FranchiseGallery_Franchise_franchise_id",
                        column: x => x.franchise_id,
                        principalSchema: "dbo",
                        principalTable: "Franchise",
                        principalColumn: "franchise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FranchiseRequestModel",
                columns: table => new
                {
                    franchise_request_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_sample_box_option = table.Column<bool>(type: "bit", nullable: false),
                    franchise_request_report = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    franchise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseRequestModel", x => x.franchise_request_id);
                    table.ForeignKey(
                        name: "FK_FranchiseRequestModel_Franchise_franchise_id",
                        column: x => x.franchise_id,
                        principalSchema: "dbo",
                        principalTable: "Franchise",
                        principalColumn: "franchise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FranchiseReview",
                schema: "dbo",
                columns: table => new
                {
                    franchise_review_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_review = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    franchise_rating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    franchise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseReview", x => x.franchise_review_id);
                    table.ForeignKey(
                        name: "FK_FranchiseReview_Franchise_franchise_id",
                        column: x => x.franchise_id,
                        principalSchema: "dbo",
                        principalTable: "Franchise",
                        principalColumn: "franchise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FranchiseService",
                schema: "dbo",
                columns: table => new
                {
                    franchise_service_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchise_provider_service_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    franchise_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FranchiseService", x => x.franchise_service_id);
                    table.ForeignKey(
                        name: "FK_FranchiseService_Franchise_franchise_id",
                        column: x => x.franchise_id,
                        principalSchema: "dbo",
                        principalTable: "Franchise",
                        principalColumn: "franchise_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Franchise_franchise_social_id",
                schema: "dbo",
                table: "Franchise",
                column: "franchise_social_id");

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseGallery_franchise_id",
                schema: "dbo",
                table: "FranchiseGallery",
                column: "franchise_id");

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseRequestModel_franchise_id",
                table: "FranchiseRequestModel",
                column: "franchise_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseReview_franchise_id",
                schema: "dbo",
                table: "FranchiseReview",
                column: "franchise_id");

            migrationBuilder.CreateIndex(
                name: "IX_FranchiseService_franchise_id",
                schema: "dbo",
                table: "FranchiseService",
                column: "franchise_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FranchiseGallery",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FranchiseRequestModel");

            migrationBuilder.DropTable(
                name: "FranchiseReview",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FranchiseService",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Franchise",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FranchiseSocial",
                schema: "dbo");
        }
    }
}
