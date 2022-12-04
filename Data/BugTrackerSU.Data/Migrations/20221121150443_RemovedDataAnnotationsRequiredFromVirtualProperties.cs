using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerSU.Data.Migrations
{
    public partial class RemovedDataAnnotationsRequiredFromVirtualProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectMangaerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects",
                column: "ProjectMangaerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects");

            migrationBuilder.AlterColumn<string>(
                name: "ProjectMangaerId",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectMangaerId",
                table: "Projects",
                column: "ProjectMangaerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
