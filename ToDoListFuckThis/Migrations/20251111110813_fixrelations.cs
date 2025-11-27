using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListFuckThis.Migrations
{
    /// <inheritdoc />
    public partial class fixrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todolists_user_UserId",
                table: "todolists");

            migrationBuilder.DropIndex(
                name: "IX_todolists_UserId",
                table: "todolists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "todolists");

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "todolists",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_todolists_UsersId",
                table: "todolists",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_todolists_user_UsersId",
                table: "todolists",
                column: "UsersId",
                principalTable: "user",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_todolists_user_UsersId",
                table: "todolists");

            migrationBuilder.DropIndex(
                name: "IX_todolists_UsersId",
                table: "todolists");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "todolists");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "todolists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_todolists_UserId",
                table: "todolists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_todolists_user_UserId",
                table: "todolists",
                column: "UserId",
                principalTable: "user",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
