using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class addedquests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid[]>(
                name: "CompletedQuests",
                table: "Characters",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Achievements_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestGoal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestGoal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    QuestGoalId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Quest_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Quest_QuestGoal_QuestGoalId",
                        column: x => x.QuestGoalId,
                        principalTable: "QuestGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestSection",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Question = table.Column<string>(type: "text", nullable: false),
                    Answer = table.Column<string>(type: "text", nullable: false),
                    Hint = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    SectionGoalId = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestSection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestSection_QuestGoal_SectionGoalId",
                        column: x => x.SectionGoalId,
                        principalTable: "QuestGoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestSection_Quest_QuestId",
                        column: x => x.QuestId,
                        principalTable: "Quest",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestStep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false),
                    QuestSectionId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestStep_QuestSection_QuestSectionId",
                        column: x => x.QuestSectionId,
                        principalTable: "QuestSection",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Achievements_CharacterId",
                table: "Achievements",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_CharacterId",
                table: "Quest",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Quest_QuestGoalId",
                table: "Quest",
                column: "QuestGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestSection_QuestId",
                table: "QuestSection",
                column: "QuestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestSection_SectionGoalId",
                table: "QuestSection",
                column: "SectionGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestStep_QuestSectionId",
                table: "QuestStep",
                column: "QuestSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "QuestStep");

            migrationBuilder.DropTable(
                name: "QuestSection");

            migrationBuilder.DropTable(
                name: "Quest");

            migrationBuilder.DropTable(
                name: "QuestGoal");

            migrationBuilder.DropColumn(
                name: "CompletedQuests",
                table: "Characters");
        }
    }
}
