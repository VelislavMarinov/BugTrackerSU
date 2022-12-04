using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerSU.Data.Migrations
{
    public partial class addedPropertyProjectMangaerIdInProjectModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectManagerId",
                table: "Projects");
        }
    }
}
