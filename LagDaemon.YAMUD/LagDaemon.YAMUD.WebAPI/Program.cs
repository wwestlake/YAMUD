using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services.LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Data.Repositories;
using LagDaemon.YAMUD.Services;
using LagDaemon.YAMUD.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentEmail.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
    .Build();
builder.Services.AddSingleton(configuration);
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

foreach (var config in configuration.AsEnumerable())
{
    Console.WriteLine($"{config.Key}: {config.Value}");
}

var connectionString = configuration.GetConnectionString("postgres");
builder.Services.AddDbContext<YamudDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IFluentEmailFactory, YamudFluentEmailFactory>();

builder.Services.AddSingleton<IHostEnvironment>(provider =>
{
    var env = provider.GetRequiredService<IServiceProvider>().GetRequiredService<IHostEnvironment>();
    return env;
});

builder.Services.AddTransient<IEmailService, EmailService>();
// JWT authentication setup
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443; // Set the HTTPS port
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
