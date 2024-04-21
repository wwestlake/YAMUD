using LagDaemon.YAMUD.Model.GameCommands;

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
            { "exit", CommandToken.Exit },
            { "msg", CommandToken.Message }
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
                    return new GoCommand { Parameters = parameters };
                case CommandToken.Look:
                    return new LookCommand { Parameters = parameters };
                case CommandToken.Examine:
                    return new ExamineCommand { Parameters = parameters };
                case CommandToken.Take:
                    return new TakeCommand { Parameters = parameters };
                case CommandToken.Drop:
                    return new DropCommand { Parameters = parameters };
                case CommandToken.Inventory:
                case CommandToken.Inv:
                    return new InventoryCommand { Parameters = parameters };
                case CommandToken.Use:
                    return new UseCommand { Parameters = parameters };
                case CommandToken.Give:
                    return new GiveCommand { Parameters = parameters };
                case CommandToken.Equip:
                    return new EquipCommand { Parameters = parameters };
                case CommandToken.Unequip:
                    return new UnequipCommand { Parameters = parameters };
                case CommandToken.Say:
                    return new SayCommand { Parameters = parameters };
                case CommandToken.Tell:
                    return new TellCommand { Parameters = parameters };
                case CommandToken.Emote:
                    return new EmoteCommand { Parameters = parameters };
                case CommandToken.Whisper:
                    return new WhisperCommand { Parameters = parameters };
                case CommandToken.Shout:
                    return new ShoutCommand { Parameters = parameters };
                case CommandToken.Reply:
                    return new ReplyCommand { Parameters = parameters };
                case CommandToken.TalkTo:
                    return new TalkToCommand { Parameters = parameters };
                case CommandToken.Attack:
                    return new AttackCommand { Parameters = parameters };
                case CommandToken.Score:
                    return new ScoreCommand { Parameters = parameters };
                case CommandToken.Help:
                    return new HelpCommand { Parameters = parameters };
                case CommandToken.Who:
                    return new WhoCommand { Parameters = parameters };
                case CommandToken.Where:
                    return new WhereCommand { Parameters = parameters };
                case CommandToken.Time:
                    return new TimeCommand { Parameters = parameters };
                case CommandToken.Kick:
                    return new KickCommand { Parameters = parameters };
                case CommandToken.Ban:
                    return new BanCommand { Parameters = parameters };
                case CommandToken.Mute:
                    return new MuteCommand { Parameters = parameters };
                case CommandToken.Set:
                    return new SetCommand { Parameters = parameters };
                case CommandToken.Jail:
                    return new JailCommand { Parameters = parameters };
                case CommandToken.Alias:
                    return new AliasCommand { Parameters = parameters };
                case CommandToken.Message:
                    return new MessageCommand { Parameters = parameters };
                default: return new Command { Parameters = parameters };
            }
        }
    }
}