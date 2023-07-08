using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccoutGRPCService.Migrations
{
    public partial class newfieldprofilephotoadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profile_photo_url",
                schema: "dbo",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_photo_url",
                schema: "dbo",
                table: "User");
        }
    }
}
