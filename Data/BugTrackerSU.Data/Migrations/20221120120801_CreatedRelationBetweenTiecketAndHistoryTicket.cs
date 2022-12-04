using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerSU.Data.Migrations
{
    public partial class CreatedRelationBetweenTiecketAndHistoryTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "TicketsHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProjectMangaerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TicketsHistories_TicketId",
                table: "TicketsHistories",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectMangaerId",
                table: "Projects",
                column: "ProjectMangaerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects",
                column: "ProjectMangaerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketsHistories_Tickets_TicketId",
                table: "TicketsHistories",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketsHistories_Tickets_TicketId",
                table: "TicketsHistories");

            migrationBuilder.DropIndex(
                name: "IX_TicketsHistories_TicketId",
                table: "TicketsHistories");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectMangaerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketsHistories");

            migrationBuilder.DropColumn(
                name: "ProjectMangaerId",
                table: "Projects");
        }
    }
}
