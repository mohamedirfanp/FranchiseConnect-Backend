﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseGRPCService.Migrations
{
    public partial class IncreasedFranchiseAboutSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "franchise_about",
                schema: "dbo",
                table: "Franchise",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "franchise_about",
                schema: "dbo",
                table: "Franchise",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);
        }
    }
}
