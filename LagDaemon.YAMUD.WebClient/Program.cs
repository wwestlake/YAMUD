using LagDaemon.YAMUD.Model.Scripting;
using LagDaemon.YAMUD.WebClient;
using LagDaemon.YAMUD.WebClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using static System.Net.WebRequestMethods;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var hubAddress = builder.HostEnvironment.BaseAddress.Replace("7202", "7214");

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IIndexedDbService, IndexedDbService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IScriptingModuleService, ScriptingModuleService>();
builder.Services.AddScoped<IPluginsService, PluginsService>();


builder.Services.AddSingleton<ChatService>(provider => new ChatService($"{hubAddress}chatHub/"));
builder.Services.AddSingleton<CommandService>(provider => new CommandService($"{hubAddress}commandHub/"));
builder.Services.AddSingleton<CommandParser>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<Authority>();

var host = builder.Build();
var localStorageService = host.Services.GetRequiredService<ILocalStorageService>();

// Start the token expiration check
var cancellationTokenSource = new CancellationTokenSource();
var tokenExpirationTask = localStorageService.CheckTokenExpirationAsync(cancellationTokenSource.Token);

// Run the Blazor application
await host.RunAsync();
