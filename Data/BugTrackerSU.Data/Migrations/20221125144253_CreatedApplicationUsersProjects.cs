using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTrackerSU.Data.Migrations
{
    public partial class CreatedApplicationUsersProjects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserProject_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserProject");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserProject_Projects_ProjectId",
                table: "ApplicationUserProject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserProject",
                table: "ApplicationUserProject");

            migrationBuilder.RenameTable(
                name: "ApplicationUserProject",
                newName: "ApplicationUsersProjects");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserProject_ProjectId",
                table: "ApplicationUsersProjects",
                newName: "IX_ApplicationUsersProjects_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserProject_IsDeleted",
                table: "ApplicationUsersProjects",
                newName: "IX_ApplicationUsersProjects_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserProject_ApplicationUserId",
                table: "ApplicationUsersProjects",
                newName: "IX_ApplicationUsersProjects_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUsersProjects",
                table: "ApplicationUsersProjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersProjects_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersProjects",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsersProjects_Projects_ProjectId",
                table: "ApplicationUsersProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersProjects_AspNetUsers_ApplicationUserId",
                table: "ApplicationUsersProjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsersProjects_Projects_ProjectId",
                table: "ApplicationUsersProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUsersProjects",
                table: "ApplicationUsersProjects");

            migrationBuilder.RenameTable(
                name: "ApplicationUsersProjects",
                newName: "ApplicationUserProject");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsersProjects_ProjectId",
                table: "ApplicationUserProject",
                newName: "IX_ApplicationUserProject_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsersProjects_IsDeleted",
                table: "ApplicationUserProject",
                newName: "IX_ApplicationUserProject_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUsersProjects_ApplicationUserId",
                table: "ApplicationUserProject",
                newName: "IX_ApplicationUserProject_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserProject",
                table: "ApplicationUserProject",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserProject_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserProject",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserProject_Projects_ProjectId",
                table: "ApplicationUserProject",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
