using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class itemsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemBase");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlayerStateId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_PlayerState_PlayerStateId",
                        column: x => x.PlayerStateId,
                        principalTable: "PlayerState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlayerStateId",
                table: "Item",
                column: "PlayerStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.CreateTable(
                name: "ItemBase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CanConsume = table.Column<bool>(type: "boolean", nullable: false),
                    CanDestroy = table.Column<bool>(type: "boolean", nullable: false),
                    CanDrop = table.Column<bool>(type: "boolean", nullable: false),
                    CanPlace = table.Column<bool>(type: "boolean", nullable: false),
                    CanSpoil = table.Column<bool>(type: "boolean", nullable: false),
                    CanTake = table.Column<bool>(type: "boolean", nullable: false),
                    CanUse = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MaxStackSize = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PlayerStateId = table.Column<Guid>(type: "uuid", nullable: true),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBase_PlayerState_PlayerStateId",
                        column: x => x.PlayerStateId,
                        principalTable: "PlayerState",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemBase_PlayerStateId",
                table: "ItemBase",
                column: "PlayerStateId");
        }
    }
}
