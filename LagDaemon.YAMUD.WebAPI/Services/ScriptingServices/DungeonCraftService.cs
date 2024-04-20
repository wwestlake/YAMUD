using LagDaemon.YAMUD.Model.Scripting;

namespace LagDaemon.YAMUD.WebAPI.Services.ScriptingServices
{
    public class DungeonCraftService
    {
        private CommandParser _commandParser;

        public DungeonCraftService(CommandParser commandParser)
        {
            _commandParser = commandParser;
        }

        public void ExecuteCommand(string command)
        {
            var commands = _commandParser.ParseCommandLine(command);
            foreach (var cmd in commands)
            {
                // Execute the command
            }
        }

    }
}
