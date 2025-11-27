using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListFuckThis.Migrations
{
    /// <inheritdoc />
    public partial class updateTodoList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_projects_ProjectsId",
                table: "user");

            migrationBuilder.DropIndex(
                name: "IX_user_ProjectsId",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "user");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "timeEnd",
                table: "todolists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "timeStart",
                table: "todolists",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ProjectsUsers",
                columns: table => new
                {
                    ProjectsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectsUsers", x => new { x.ProjectsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ProjectsUsers_projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectsUsers_user_UsersId",
                        column: x => x.UsersId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectsUsers_UsersId",
                table: "ProjectsUsers",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectsUsers");

            migrationBuilder.DropColumn(
                name: "timeEnd",
                table: "todolists");

            migrationBuilder.DropColumn(
                name: "timeStart",
                table: "todolists");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "user",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectsId",
                table: "user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_ProjectsId",
                table: "user",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_projects_ProjectsId",
                table: "user",
                column: "ProjectsId",
                principalTable: "projects",
                principalColumn: "Id");
        }
    }
}
