using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Culture.DataAccess.Migrations
{
    public partial class updateeventmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "EventReactions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Emoticons");

            migrationBuilder.AddColumn<DateTime>(
                name: "TakesPlaceDate",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "EventReactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NameId",
                table: "Emoticons",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emoticons_NameId",
                table: "Emoticons",
                column: "NameId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emoticons_Emoticons_NameId",
                table: "Emoticons",
                column: "NameId",
                principalTable: "Emoticons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emoticons_Emoticons_NameId",
                table: "Emoticons");

            migrationBuilder.DropIndex(
                name: "IX_Emoticons_NameId",
                table: "Emoticons");

            migrationBuilder.DropColumn(
                name: "TakesPlaceDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EventReactions");

            migrationBuilder.DropColumn(
                name: "NameId",
                table: "Emoticons");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "EventReactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Emoticons",
                nullable: true);
        }
    }
}
