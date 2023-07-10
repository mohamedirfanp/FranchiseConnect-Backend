using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatGRPCService.Migrations
{
    public partial class newfieldaddedtoConversionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_blocked",
                table: "ConversationModel",
                newName: "is_accepted");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "ConversationModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "franchisor_name",
                table: "ConversationModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "ConversationModel");

            migrationBuilder.DropColumn(
                name: "franchisor_name",
                table: "ConversationModel");

            migrationBuilder.RenameColumn(
                name: "is_accepted",
                table: "ConversationModel",
                newName: "is_blocked");
        }
    }
}
