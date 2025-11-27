using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListFuckThis.Migrations
{
    /// <inheritdoc />
    public partial class aaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoSections_projects_ProjectsId",
                table: "todoSections");

            migrationBuilder.DropIndex(
                name: "IX_todoSections_ProjectsId",
                table: "todoSections");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "todoSections");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "todoSections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_todoSections_ProjectId",
                table: "todoSections",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_todoSections_projects_ProjectId",
                table: "todoSections",
                column: "ProjectId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todoSections_projects_ProjectId",
                table: "todoSections");

            migrationBuilder.DropIndex(
                name: "IX_todoSections_ProjectId",
                table: "todoSections");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "todoSections");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectsId",
                table: "todoSections",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_todoSections_ProjectsId",
                table: "todoSections",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_todoSections_projects_ProjectsId",
                table: "todoSections",
                column: "ProjectsId",
                principalTable: "projects",
                principalColumn: "Id");
        }
    }
}
