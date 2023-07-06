using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatGRPCService.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConversationModel",
                columns: table => new
                {
                    conversation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    franchisee_id = table.Column<int>(type: "int", nullable: false),
                    franchisor_id = table.Column<int>(type: "int", nullable: false),
                    is_blocked = table.Column<bool>(type: "bit", nullable: false),
                    franchisee_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    franchisor_franchise_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationModel", x => x.conversation_id);
                });

            migrationBuilder.CreateTable(
                name: "QueryModel",
                columns: table => new
                {
                    query_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    query_title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    query_type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    query_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueryModel", x => x.query_id);
                });

            migrationBuilder.CreateTable(
                name: "TimelineCommentModel",
                columns: table => new
                {
                    timeline_comment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    from_id = table.Column<int>(type: "int", nullable: false),
                    from_role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    QueryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimelineCommentModel", x => x.timeline_comment_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversationModel");

            migrationBuilder.DropTable(
                name: "QueryModel");

            migrationBuilder.DropTable(
                name: "TimelineCommentModel");
        }
    }
}
