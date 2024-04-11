using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class chageroomexitstype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Exits_ExitsId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Exits");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_ExitsId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ExitsId",
                table: "Rooms");

            migrationBuilder.CreateTable(
                name: "Exit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ToRoom = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exit_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exit_RoomId",
                table: "Exit",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exit");

            migrationBuilder.AddColumn<Guid>(
                name: "ExitsId",
                table: "Rooms",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Exits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Direction = table.Column<int>(type: "integer", nullable: false),
                    ToRoom = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_ExitsId",
                table: "Rooms",
                column: "ExitsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Exits_ExitsId",
                table: "Rooms",
                column: "ExitsId",
                principalTable: "Exits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
