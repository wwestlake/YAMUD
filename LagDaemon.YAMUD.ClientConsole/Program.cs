﻿
using LagDaemon.YAMUD.ClientConsole.UserAccount;
using LagDaemon.YAMUD.Model.Scripting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var apiUri = new Uri(configuration["ApiUrl"]);

// Set up DI container
var builder = new ServiceCollection();
builder
    .AddSingleton(configuration)
    .AddSingleton<CommandParser>()
    .AddSingleton<AccountManager>()
    .AddSingleton<Application>();

builder
    .AddHttpClient("MudHub", client => {
         client.BaseAddress = new Uri(configuration["ApiUrl"]);
     });

builder.BuildServiceProvider();

var serviceProvider = builder.BuildServiceProvider();

// Start your application logic
var app = serviceProvider.GetRequiredService<Application>();

app.Run();

