using Microsoft.EntityFrameworkCore.Migrations;

namespace Culture.DataAccess.Migrations
{
    public partial class deleteeventupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsInCalendar_Events_EventId",
                table: "EventsInCalendar");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsInCalendar_Events_EventId",
                table: "EventsInCalendar",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsInCalendar_Events_EventId",
                table: "EventsInCalendar");

            migrationBuilder.AddForeignKey(
                name: "FK_EventsInCalendar_Events_EventId",
                table: "EventsInCalendar",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
