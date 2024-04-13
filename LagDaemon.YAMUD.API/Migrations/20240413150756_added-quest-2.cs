using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagDaemon.YAMUD.API.Migrations
{
    /// <inheritdoc />
    public partial class addedquest2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quest_Characters_CharacterId",
                table: "Quest");

            migrationBuilder.DropForeignKey(
                name: "FK_Quest_QuestGoal_QuestGoalId",
                table: "Quest");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestSection_QuestGoal_SectionGoalId",
                table: "QuestSection");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestSection_Quest_QuestId",
                table: "QuestSection");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestStep_QuestSection_QuestSectionId",
                table: "QuestStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestStep",
                table: "QuestStep");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestSection",
                table: "QuestSection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quest",
                table: "Quest");

            migrationBuilder.RenameTable(
                name: "QuestStep",
                newName: "QuestSteps");

            migrationBuilder.RenameTable(
                name: "QuestSection",
                newName: "QuestSections");

            migrationBuilder.RenameTable(
                name: "Quest",
                newName: "Quests");

            migrationBuilder.RenameIndex(
                name: "IX_QuestStep_QuestSectionId",
                table: "QuestSteps",
                newName: "IX_QuestSteps_QuestSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestSection_SectionGoalId",
                table: "QuestSections",
                newName: "IX_QuestSections_SectionGoalId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestSection_QuestId",
                table: "QuestSections",
                newName: "IX_QuestSections_QuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Quest_QuestGoalId",
                table: "Quests",
                newName: "IX_Quests_QuestGoalId");

            migrationBuilder.RenameIndex(
                name: "IX_Quest_CharacterId",
                table: "Quests",
                newName: "IX_Quests_CharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestSteps",
                table: "QuestSteps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestSections",
                table: "QuestSections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quests",
                table: "Quests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestSections_QuestGoal_SectionGoalId",
                table: "QuestSections",
                column: "SectionGoalId",
                principalTable: "QuestGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestSections_Quests_QuestId",
                table: "QuestSections",
                column: "QuestId",
                principalTable: "Quests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestSteps_QuestSections_QuestSectionId",
                table: "QuestSteps",
                column: "QuestSectionId",
                principalTable: "QuestSections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_Characters_CharacterId",
                table: "Quests",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quests_QuestGoal_QuestGoalId",
                table: "Quests",
                column: "QuestGoalId",
                principalTable: "QuestGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestSections_QuestGoal_SectionGoalId",
                table: "QuestSections");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestSections_Quests_QuestId",
                table: "QuestSections");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestSteps_QuestSections_QuestSectionId",
                table: "QuestSteps");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_Characters_CharacterId",
                table: "Quests");

            migrationBuilder.DropForeignKey(
                name: "FK_Quests_QuestGoal_QuestGoalId",
                table: "Quests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Quests",
                table: "Quests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestSteps",
                table: "QuestSteps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestSections",
                table: "QuestSections");

            migrationBuilder.RenameTable(
                name: "Quests",
                newName: "Quest");

            migrationBuilder.RenameTable(
                name: "QuestSteps",
                newName: "QuestStep");

            migrationBuilder.RenameTable(
                name: "QuestSections",
                newName: "QuestSection");

            migrationBuilder.RenameIndex(
                name: "IX_Quests_QuestGoalId",
                table: "Quest",
                newName: "IX_Quest_QuestGoalId");

            migrationBuilder.RenameIndex(
                name: "IX_Quests_CharacterId",
                table: "Quest",
                newName: "IX_Quest_CharacterId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestSteps_QuestSectionId",
                table: "QuestStep",
                newName: "IX_QuestStep_QuestSectionId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestSections_SectionGoalId",
                table: "QuestSection",
                newName: "IX_QuestSection_SectionGoalId");

            migrationBuilder.RenameIndex(
                name: "IX_QuestSections_QuestId",
                table: "QuestSection",
                newName: "IX_QuestSection_QuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Quest",
                table: "Quest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestStep",
                table: "QuestStep",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestSection",
                table: "QuestSection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_Characters_CharacterId",
                table: "Quest",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Quest_QuestGoal_QuestGoalId",
                table: "Quest",
                column: "QuestGoalId",
                principalTable: "QuestGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestSection_QuestGoal_SectionGoalId",
                table: "QuestSection",
                column: "SectionGoalId",
                principalTable: "QuestGoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestSection_Quest_QuestId",
                table: "QuestSection",
                column: "QuestId",
                principalTable: "Quest",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestStep_QuestSection_QuestSectionId",
                table: "QuestStep",
                column: "QuestSectionId",
                principalTable: "QuestSection",
                principalColumn: "Id");
        }
    }
}
