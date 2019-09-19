using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Culture.DataAccess.Migrations
{
    public partial class filestorechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Events",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Events");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Events",
                nullable: false);
        }
    }
}
