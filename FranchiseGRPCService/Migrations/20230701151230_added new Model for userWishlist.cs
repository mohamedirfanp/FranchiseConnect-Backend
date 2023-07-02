using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class addednewModelforuserWishlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "FranchiseRequestModel",
                newName: "owner_id");

            migrationBuilder.AddColumn<string>(
                name: "is_request_status",
                table: "FranchiseRequestModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_request_status",
                table: "FranchiseRequestModel");

            migrationBuilder.RenameColumn(
                name: "owner_id",
                table: "FranchiseRequestModel",
                newName: "user_id");
        }
    }
}
