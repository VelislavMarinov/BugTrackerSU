using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerSU.Data.Migrations
{
    public partial class RenmaedPropertyProjectManagerInProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectMangaerId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectMangaerId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectManagerId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectManagerId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ProjectMangaerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectMangaerId",
                table: "Projects",
                column: "ProjectMangaerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects",
                column: "ProjectMangaerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
