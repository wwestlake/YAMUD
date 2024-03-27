using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class modifieditembase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanConsume",
                table: "ItemBase",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanDestroy",
                table: "ItemBase",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanDrop",
                table: "ItemBase",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanPlace",
                table: "ItemBase",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanSpoil",
                table: "ItemBase",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanTake",
                table: "ItemBase",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanUse",
                table: "ItemBase",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxStackSize",
                table: "ItemBase",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Value",
                table: "ItemBase",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "ItemBase",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanConsume",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "CanDestroy",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "CanDrop",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "CanPlace",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "CanSpoil",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "CanTake",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "CanUse",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "MaxStackSize",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ItemBase");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "ItemBase");
        }
    }
}
