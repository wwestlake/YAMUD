using LagDaemon.YAMUD.ClientConsole.ConsoleIO;
using LagDaemon.YAMUD.ClientConsole.Services;
using LagDaemon.YAMUD.ClientConsole.UserAccount;
using LagDaemon.YAMUD.Model.GameCommands;
using LagDaemon.YAMUD.Model.Scripting;
using LagDaemon.YAMUD.Model.Utilities;
using Microsoft.Extensions.Configuration;

internal class Application
{
    private bool _isRunning = true;
    GameContext _context;
    private HttpClient _mudHubClient;
    private AccountManager _accountManager;
    private CommandParser _commandParser;
    private CommandService _commandService;
    private IConfiguration _configuration;
    private ConsoleInputHandler _consoleHander = new ConsoleInputHandler();
    private bool _authenticated = false;
    private string _token = string.Empty;
    private Menu _topMenu;

    public Application(
        IHttpClientFactory clientFactory, 
        AccountManager accountManager, 
        CommandParser commandParser, 
        CommandService commandService,
        IConfiguration configuration)
    {
        _mudHubClient = clientFactory.CreateClient("MudHub");
        _accountManager = accountManager;
        _commandParser = commandParser;
        _commandService = commandService;
        _configuration = configuration;

        _commandService.CommandReceived += (command) =>
        {
            Console.WriteLine($"Received command: {command.Type}");
            foreach (var parameter in command.Parameters)
            {
                Console.WriteLine($"Parameter: {parameter}");
            }
        };

        _context = new GameContext() { CurrentUser = new LagDaemon.YAMUD.Model.User.UserAccount(), Actor = new LagDaemon.YAMUD.Model.Characters.Character() };
        _topMenu = new Menu("Select> ", new List<Menu.MenuItem>() { 
            new Menu.MenuItem() { 
                Entry = "Login to existing account", 
                Action = async () => { 
                    _authenticated = _accountManager.AuthenticateUser().Result;
                    if (_authenticated) await RunRepl();
                    else _isRunning = false;
                }
            },
            new Menu.MenuItem() { 
                Entry = "Create new account", 
                Action = async () => { 
                    _authenticated = _accountManager.CreateAccount().Result;
                    if (_authenticated) await RunRepl();
                    else _isRunning = false;
                } 
            },
            new Menu.MenuItem() { 
                Entry = "Exit", 
                Action = () => { 
                    _isRunning = false; 
                } 
            }
        });
    }

    public void Run()
    {

        Console.WriteLine("Starting application...");

        // Start REPL loop on a worker thread
        Task.Run(StartReplLoop);

        // You can add any other initialization logic here

        // Wait for user to exit
        while (_isRunning)
        {
            Thread.Sleep(1000);
        }
        Console.WriteLine("Application exiting");
    }

    private async Task StartReplLoop()
    {
        Console.WriteLine("REPL loop started.");

        if (_configuration.GetSection("autologin").Exists() )
        {
            var userId = _configuration["autologin:userId"];
            var password = _configuration["autologin:password"];

            _authenticated = _accountManager.AuthenticateUser(userId, password).Result;
        }
        if (!_authenticated)
        {
            while (_topMenu.Display()) { /* Just repeats */ }
        } else
        {
            await RunRepl();
        }

    }

    private async Task RunRepl()
    {
        _isRunning = _authenticated;
        while (_authenticated)
        {
            var input = _consoleHander.GetInput("yamud> ");

            // Parse and process the input
            var commands = _commandParser.ParseCommandLine(input);
            foreach (var command in commands)
            {
                if (! command.Validate(_context))
                {
                    Console.WriteLine("Invalid command");
                    continue;
                }
                if (command.Type == CommandToken.Exit)
                {
                    Console.WriteLine("Exiting application...");
                    _isRunning = false;
                    return;
                }
                ProcessCommand(command);
            }
        }

    }

    private void ProcessCommand(Command command)
    {
        // Process the command based on its type and parameters
        Console.WriteLine($"Processing command: {command.Type}");
        foreach (var parameter in command.Parameters)
        {
            Console.WriteLine($"Parameter: {parameter}");
        }
        Console.WriteLine("Sending Command to server:");
        _commandService.SendCommand(command).Wait();
    }

}



