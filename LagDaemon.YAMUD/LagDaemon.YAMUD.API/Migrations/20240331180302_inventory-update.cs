using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class inventoryupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "UserAccounts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "Item",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WearAndTear",
                table: "Item",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Weight",
                table: "Item",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_InventoryId",
                table: "UserAccounts",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_InventoryId",
                table: "Item",
                column: "InventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Inventory_InventoryId",
                table: "Item",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_Inventory_InventoryId",
                table: "UserAccounts",
                column: "InventoryId",
                principalTable: "Inventory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Inventory_InventoryId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_Inventory_InventoryId",
                table: "UserAccounts");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_InventoryId",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_Item_InventoryId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "WearAndTear",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Item");
        }
    }
}
