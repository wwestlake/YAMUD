using LagDaemon.YAMUD.Model.Characters;

namespace LagDaemon.YAMUD.Automation
{
    public interface IQuestPlugin : IPlugin
    {
        void OnQuestProgress(Character character, QuestProgress progress);
        // Other methods related to quests
    }
}
