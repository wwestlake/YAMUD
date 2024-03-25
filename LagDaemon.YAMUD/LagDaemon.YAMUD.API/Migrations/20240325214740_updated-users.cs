using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class updatedusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_PlayerState_PlayerStateId",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_PlayerStateId",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "PlayerStateId",
                table: "UserAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerState_UserAccounts_Id",
                table: "PlayerState",
                column: "Id",
                principalTable: "UserAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerState_UserAccounts_Id",
                table: "PlayerState");

            migrationBuilder.AddColumn<Guid>(
                name: "PlayerStateId",
                table: "UserAccounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_PlayerStateId",
                table: "UserAccounts",
                column: "PlayerStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_PlayerState_PlayerStateId",
                table: "UserAccounts",
                column: "PlayerStateId",
                principalTable: "PlayerState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
