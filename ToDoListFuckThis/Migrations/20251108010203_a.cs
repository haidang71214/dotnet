using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListFuckThis.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
