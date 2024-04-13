using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingquest3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Question",
                table: "QuestSections",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Answer",
                table: "QuestSections",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "QuestSections",
                newName: "Question");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "QuestSections",
                newName: "Answer");
        }
    }
}
