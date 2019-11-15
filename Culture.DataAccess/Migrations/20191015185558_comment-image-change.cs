using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Culture.DataAccess.Migrations
{
    public partial class commentimagechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Comments");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Comments",
                nullable: true);
        }
    }
}
