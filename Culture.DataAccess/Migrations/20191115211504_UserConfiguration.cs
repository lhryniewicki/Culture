using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Culture.DataAccess.Migrations
{
    public partial class UserConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserConfigurationId",
                table: "AspNetUsers",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "UserConfigurations",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    CommentsDisplayAmount = table.Column<int>(nullable: false),
                    EventsDisplayAmount = table.Column<int>(nullable: false),
                    LogOutAfter = table.Column<int>(nullable: false),
                    Anonymous = table.Column<bool>(nullable: false),
                    SendEmailNotification = table.Column<bool>(nullable: false),
                    CalendarPastEvents = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfigurations", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserConfigurations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserConfigurations");

            migrationBuilder.DropColumn(
                name: "UserConfigurationId",
                table: "AspNetUsers");
        }
    }
}
