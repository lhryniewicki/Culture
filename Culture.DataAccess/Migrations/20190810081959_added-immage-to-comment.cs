using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Culture.DataAccess.Migrations
{
    public partial class addedimmagetocomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Comments");
        }
    }
}
