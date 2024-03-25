using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class updatedforeignkeyforplayerstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerState_UserAccounts_Id",
                table: "PlayerState");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlayerState",
                newName: "UserAccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserAccountID",
                table: "PlayerState",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_UserAccountID",
                table: "PlayerState",
                column: "UserAccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_UserAccountId",
                table: "PlayerState",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerState_UserAccounts_UserAccountID",
                table: "PlayerState",
                column: "UserAccountID",
                principalTable: "UserAccounts",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerState_UserAccounts_UserAccountId",
                table: "PlayerState",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerState_UserAccounts_UserAccountID",
                table: "PlayerState");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerState_UserAccounts_UserAccountId",
                table: "PlayerState");

            migrationBuilder.DropIndex(
                name: "IX_PlayerState_UserAccountID",
                table: "PlayerState");

            migrationBuilder.DropIndex(
                name: "IX_PlayerState_UserAccountId",
                table: "PlayerState");

            migrationBuilder.DropColumn(
                name: "UserAccountID",
                table: "PlayerState");

            migrationBuilder.RenameColumn(
                name: "UserAccountId",
                table: "PlayerState",
                newName: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerState_UserAccounts_Id",
                table: "PlayerState",
                column: "Id",
                principalTable: "UserAccounts",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
