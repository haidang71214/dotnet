using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListFuckThis.Migrations
{
    /// <inheritdoc />
    public partial class fixrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projectRoles_projects_projectsId",
                table: "projectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_projectRoles_user_userId",
                table: "projectRoles");

            migrationBuilder.DropIndex(
                name: "IX_projectRoles_userId",
                table: "projectRoles");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "projectRoles");

            migrationBuilder.RenameColumn(
                name: "projectsId",
                table: "projectRoles",
                newName: "ProjectsId");

            migrationBuilder.RenameIndex(
                name: "IX_projectRoles_projectsId",
                table: "projectRoles",
                newName: "IX_projectRoles_ProjectsId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "todolists",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectsId",
                table: "projectRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "projectRoles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_projectRoles_UsersId",
                table: "projectRoles",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_projectRoles_projects_ProjectsId",
                table: "projectRoles",
                column: "ProjectsId",
                principalTable: "projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_projectRoles_user_UsersId",
                table: "projectRoles",
                column: "UsersId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_projectRoles_projects_ProjectsId",
                table: "projectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_projectRoles_user_UsersId",
                table: "projectRoles");

            migrationBuilder.DropIndex(
                name: "IX_projectRoles_UsersId",
                table: "projectRoles");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "projectRoles");

            migrationBuilder.RenameColumn(
                name: "ProjectsId",
                table: "projectRoles",
                newName: "projectsId");

            migrationBuilder.RenameIndex(
                name: "IX_projectRoles_ProjectsId",
                table: "projectRoles",
                newName: "IX_projectRoles_projectsId");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "todolists",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "projectsId",
                table: "projectRoles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "userId",
                table: "projectRoles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_projectRoles_userId",
                table: "projectRoles",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_projectRoles_projects_projectsId",
                table: "projectRoles",
                column: "projectsId",
                principalTable: "projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_projectRoles_user_userId",
                table: "projectRoles",
                column: "userId",
                principalTable: "user",
                principalColumn: "Id");
        }
    }
}
