using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListFuckThis.Migrations
{
    /// <inheritdoc />
    public partial class updateshitdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoSections_projects_ProjectId",
                table: "todoSections");

            migrationBuilder.DropTable(
                name: "projectRoles");

            migrationBuilder.DropIndex(
                name: "IX_todoSections_ProjectId",
                table: "todoSections");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectsId",
                table: "todoSections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_todoSections_ProjectsId",
                table: "todoSections",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_OwnerId",
                table: "projects",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_projects_user_OwnerId",
                table: "projects",
                column: "OwnerId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_todoSections_projects_ProjectsId",
                table: "todoSections",
                column: "ProjectsId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projects_user_OwnerId",
                table: "projects");

            migrationBuilder.DropForeignKey(
                name: "FK_todoSections_projects_ProjectsId",
                table: "todoSections");

            migrationBuilder.DropIndex(
                name: "IX_todoSections_ProjectsId",
                table: "todoSections");

            migrationBuilder.DropIndex(
                name: "IX_projects_OwnerId",
                table: "projects");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "todoSections");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "projects");

            migrationBuilder.CreateTable(
                name: "projectRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false),
                    roleProject = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projectRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projectRoles_projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_projectRoles_user_UsersId",
                        column: x => x.UsersId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_todoSections_ProjectId",
                table: "todoSections",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_projectRoles_ProjectsId",
                table: "projectRoles",
                column: "ProjectsId");

            migrationBuilder.CreateIndex(
                name: "IX_projectRoles_UsersId",
                table: "projectRoles",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_todoSections_projects_ProjectId",
                table: "todoSections",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
