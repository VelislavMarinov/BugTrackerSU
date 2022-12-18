using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerSU.Data.Migrations
{
    public partial class CreatedConnectionBetweenMinorTaskAndApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorTasks_AspNetUsers_ApplicationUserId",
                table: "MinorTasks");

            migrationBuilder.DropIndex(
                name: "IX_MinorTasks_ApplicationUserId",
                table: "MinorTasks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "MinorTasks");

            migrationBuilder.AddColumn<string>(
                name: "AddedByUserId",
                table: "MinorTasks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MinorTasks_AddedByUserId",
                table: "MinorTasks",
                column: "AddedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorTasks_AspNetUsers_AddedByUserId",
                table: "MinorTasks",
                column: "AddedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MinorTasks_AspNetUsers_AddedByUserId",
                table: "MinorTasks");

            migrationBuilder.DropIndex(
                name: "IX_MinorTasks_AddedByUserId",
                table: "MinorTasks");

            migrationBuilder.DropColumn(
                name: "AddedByUserId",
                table: "MinorTasks");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "MinorTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MinorTasks_ApplicationUserId",
                table: "MinorTasks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MinorTasks_AspNetUsers_ApplicationUserId",
                table: "MinorTasks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
