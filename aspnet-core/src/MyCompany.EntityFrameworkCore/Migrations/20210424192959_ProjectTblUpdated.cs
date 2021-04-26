using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCompany.Migrations
{
    public partial class ProjectTblUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DevOpsId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Workspace",
                table: "Projects",
                newName: "DevOpsToken");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Projects",
                newName: "DevOpsProjectTitle");

            migrationBuilder.RenameColumn(
                name: "TasksDescription",
                table: "Projects",
                newName: "DevOpsOrganization");

            migrationBuilder.RenameColumn(
                name: "TaskTitle",
                table: "Projects",
                newName: "AsanaWorkspace");

            migrationBuilder.RenameColumn(
                name: "ProjectTitle",
                table: "Projects",
                newName: "AsanaWorkSpaceId");

            migrationBuilder.RenameColumn(
                name: "AsanaId",
                table: "Projects",
                newName: "AsanaToken");

            migrationBuilder.AddColumn<string>(
                name: "AsanaProject",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AsanaProjectId",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UpdateAsana",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "UpdateDevOps",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsanaProject",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AsanaProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdateAsana",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UpdateDevOps",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "DevOpsToken",
                table: "Projects",
                newName: "Workspace");

            migrationBuilder.RenameColumn(
                name: "DevOpsProjectTitle",
                table: "Projects",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "DevOpsOrganization",
                table: "Projects",
                newName: "TasksDescription");

            migrationBuilder.RenameColumn(
                name: "AsanaWorkspace",
                table: "Projects",
                newName: "TaskTitle");

            migrationBuilder.RenameColumn(
                name: "AsanaWorkSpaceId",
                table: "Projects",
                newName: "ProjectTitle");

            migrationBuilder.RenameColumn(
                name: "AsanaToken",
                table: "Projects",
                newName: "AsanaId");

            migrationBuilder.AddColumn<int>(
                name: "DevOpsId",
                table: "Projects",
                type: "int",
                nullable: true);
        }
    }
}
