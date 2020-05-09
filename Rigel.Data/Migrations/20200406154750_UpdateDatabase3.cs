using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rigel.Data.Migrations
{
    public partial class UpdateDatabase3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UsersId",
                table: "Todos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsersId",
                table: "Todos",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UsersId",
                table: "Todos",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UsersId",
                table: "Todos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsersId",
                table: "Todos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UsersId",
                table: "Todos",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
