using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class fixingquest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "QuestSteps",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "StepGoalId",
                table: "QuestSteps",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "QuestSections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CharacterQuestProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterQuestProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterQuestProgress_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterQuestProgressId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestProgress_CharacterQuestProgress_CharacterQuestProgress~",
                        column: x => x.CharacterQuestProgressId,
                        principalTable: "CharacterQuestProgress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SectionProgress",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompletedStepIds = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    QuestProgressId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionProgress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionProgress_QuestProgress_QuestProgressId",
                        column: x => x.QuestProgressId,
                        principalTable: "QuestProgress",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestSteps_StepGoalId",
                table: "QuestSteps",
                column: "StepGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterQuestProgress_CharacterId",
                table: "CharacterQuestProgress",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestProgress_CharacterQuestProgressId",
                table: "QuestProgress",
                column: "CharacterQuestProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionProgress_QuestProgressId",
                table: "SectionProgress",
                column: "QuestProgressId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestSteps_QuestGoal_StepGoalId",
                table: "QuestSteps",
                column: "StepGoalId",
                principalTable: "QuestGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestSteps_QuestGoal_StepGoalId",
                table: "QuestSteps");

            migrationBuilder.DropTable(
                name: "SectionProgress");

            migrationBuilder.DropTable(
                name: "QuestProgress");

            migrationBuilder.DropTable(
                name: "CharacterQuestProgress");

            migrationBuilder.DropIndex(
                name: "IX_QuestSteps_StepGoalId",
                table: "QuestSteps");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "QuestSteps");

            migrationBuilder.DropColumn(
                name: "StepGoalId",
                table: "QuestSteps");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "QuestSections");
        }
    }
}
