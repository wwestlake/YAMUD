using LagDaemon.YAMUD.Model.GameCommands;
using LagDaemon.YAMUD.Model.Map;

namespace LagDaemon.YAMUD.Model.Scripting
{
    public class CommandParser
    {
        public IEnumerable<Command> ParseCommandLine(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
            {
                return new List<Command>();
            }
            if (commandLine.Contains(";"))
            {
                return ParseMuliple(commandLine.Split(";", StringSplitOptions.RemoveEmptyEntries));
            }
            return new List<Command> { Parse(commandLine) };
        }

        public IEnumerable<Command> ParseMuliple(IEnumerable<string> commands)
        {
            return commands.Select(x => Parse(x));
        }

        public Command Parse(string input)
        {
            // Define keywords for each command type
            Dictionary<string, CommandToken> keywords = new Dictionary<string, CommandToken>
        {
            { "go", CommandToken.Go },
            { "look", CommandToken.Look },
            { "examine", CommandToken.Examine },
            { "take", CommandToken.Take },
            { "drop", CommandToken.Drop },
            { "inventory", CommandToken.Inventory },
            { "inv", CommandToken.Inv },
            { "use", CommandToken.Use },
            { "give", CommandToken.Give },
            { "equip", CommandToken.Equip },
            { "unequip", CommandToken.Unequip },
            { "say", CommandToken.Say },
            { "tell", CommandToken.Tell },
            { "emote", CommandToken.Emote },
            { "whisper", CommandToken.Whisper },
            { "shout", CommandToken.Shout },
            { "reply", CommandToken.Reply },
            { "talk to", CommandToken.TalkTo },
            { "attack", CommandToken.Attack },
            { "score", CommandToken.Score },
            { "help", CommandToken.Help },
            { "who", CommandToken.Who },
            { "where", CommandToken.Where },
            { "time", CommandToken.Time },
            { "kick", CommandToken.Kick },
            { "ban", CommandToken.Ban },
            { "mute", CommandToken.Mute },
            { "set", CommandToken.Set },
            { "jail", CommandToken.Jail },
            { "alias", CommandToken.Alias },
            { "unalias", CommandToken.Unalias },
            { "recall", CommandToken.Recall },
            { "scan", CommandToken.Scan },
            { "info", CommandToken.Info },
            { "pose", CommandToken.Pose },
            { "rp", CommandToken.Rp },
            { "whisper IC", CommandToken.WhisperIC },
            { "hug", CommandToken.Hug },
            { "kiss", CommandToken.Kiss },
            { "wave", CommandToken.Wave },
            { "dance", CommandToken.Dance },
            { "explore", CommandToken.Explore },
            { "dig", CommandToken.Dig },
            { "survey", CommandToken.Survey },
            { "custom", CommandToken.Custom },
            { "macro", CommandToken.Macro },
            { "debug", CommandToken.Debug },
            { "spawn", CommandToken.Spawn },
            { "teleport", CommandToken.Teleport },
            { "exit", CommandToken.Exit }
        };

            // Split the input string by spaces
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Determine the command type based on the first token
            CommandToken commandType = CommandToken.Custom;
            if (parts.Length > 0 && keywords.ContainsKey(parts[0]))
            {
                commandType = keywords[parts[0]];
            }

            // Extract parameters
            List<string> parameters = new List<string>();
            for (int i = 1; i < parts.Length; i++)
            {
                parameters.Add(parts[i]);
            }

            return MakeType(commandType, parameters);
        }

        private Command MakeType(CommandToken commandType, List<string> parameters)
        {
            switch (commandType)
            {
                case CommandToken.Go:
                    Direction direction;
                    if (Enum.TryParse(parameters[0], true, out direction))
                    {
                        return new GoCommand { Type = CommandToken.Go, Parameters = parameters, Direction = direction };
                    }
                    else
                    {
                        return new Command { Type = CommandToken.ERROR, Parameters = new List<string> { $"Unable to parse direction '{parameters[0]}'"  } };
                    }

                default:
                    return new Command { Type = commandType, Parameters = parameters };
            }
        }
    }

}