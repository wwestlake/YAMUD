using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class addcharclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterClass",
                table: "Characters",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CharacterClass",
                table: "Characters");
        }
    }
}
