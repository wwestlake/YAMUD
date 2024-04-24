using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class changedaddresstoowned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_RoomAddress_LocationId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_PlayerState_RoomAddress_CurrentLocationId",
                table: "PlayerState");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomAddress_AddressId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomAddress");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_AddressId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_PlayerState_CurrentLocationId",
                table: "PlayerState");

            migrationBuilder.DropIndex(
                name: "IX_Characters_LocationId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "AddressId",
                table: "Rooms",
                newName: "Address_Id");

            migrationBuilder.RenameColumn(
                name: "CurrentLocationId",
                table: "PlayerState",
                newName: "CurrentLocation_Id");

            migrationBuilder.AddColumn<int>(
                name: "Address_Level",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Address_X",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Address_Y",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLocation_Level",
                table: "PlayerState",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLocation_X",
                table: "PlayerState",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentLocation_Y",
                table: "PlayerState",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_Level",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Address_X",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Address_Y",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CurrentLocation_Level",
                table: "PlayerState");

            migrationBuilder.DropColumn(
                name: "CurrentLocation_X",
                table: "PlayerState");

            migrationBuilder.DropColumn(
                name: "CurrentLocation_Y",
                table: "PlayerState");

            migrationBuilder.RenameColumn(
                name: "Address_Id",
                table: "Rooms",
                newName: "AddressId");

            migrationBuilder.RenameColumn(
                name: "CurrentLocation_Id",
                table: "PlayerState",
                newName: "CurrentLocationId");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Characters",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RoomAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    X = table.Column<int>(type: "integer", nullable: false),
                    Y = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomAddress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_AddressId",
                table: "Rooms",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_CurrentLocationId",
                table: "PlayerState",
                column: "CurrentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_LocationId",
                table: "Characters",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_RoomAddress_LocationId",
                table: "Characters",
                column: "LocationId",
                principalTable: "RoomAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerState_RoomAddress_CurrentLocationId",
                table: "PlayerState",
                column: "CurrentLocationId",
                principalTable: "RoomAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomAddress_AddressId",
                table: "Rooms",
                column: "AddressId",
                principalTable: "RoomAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
