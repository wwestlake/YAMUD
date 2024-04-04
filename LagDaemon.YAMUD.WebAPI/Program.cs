using FluentEmail.Core;
using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Security;
using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Data.Repositories;
using LagDaemon.YAMUD.Model.User;
using LagDaemon.YAMUD.Services;
using LagDaemon.YAMUD.WebAPI.Services;
using LagDaemon.YAMUD.WebAPI.Services.CharacterServices;
using LagDaemon.YAMUD.WebAPI.Services.Scripting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
    .Build();

builder.Services.AddSingleton(configuration);

var connectionString = configuration.GetConnectionString("postgres");

// Retrieve Docker secrets as environment variables
var dbUsername = Environment.GetEnvironmentVariable("postgres_user");
var dbPassword = Environment.GetEnvironmentVariable("postgres_password");

if (string.IsNullOrEmpty(dbUsername) || string.IsNullOrEmpty(dbPassword))
{
    //throw new InvalidOperationException("Docker secrets not provided.");
}

// Modify the connection string to include the retrieved username and password
connectionString = string.Format(connectionString, dbUsername, dbPassword);

builder.Services.AddDbContext<YamudDbContext>(options =>
    options.UseNpgsql(connectionString), ServiceLifetime.Scoped);

builder.Services.AddScoped<ISecurityInterceptor, SecurityInterceptor>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISecurityProxyFactory, SecurityProxyFactory>();
builder.Services.AddScoped<UserAccountService>();
builder.Services.AddScoped<IServiceProxyFactory, ServiceProxyFactory>();
builder.Services.AddScoped<ScriptingModuleService>();
builder.Services.AddSingleton<IRandomNumberService, RandomNumberService>();
builder.Services.AddSingleton<INameGenerator, NameGenerator>();

builder.Services.AddScoped<IUserAccountService>(serviceProvider => 
    {
        var factory = serviceProvider.GetRequiredService<IServiceProxyFactory>();
        var service = serviceProvider.GetRequiredService<UserAccountService>();
        return factory.CreateProxy<IUserAccountService>(service);
    });

builder.Services.AddScoped<IScriptingModuleService>(serviceProvider =>
    {
        var factory = serviceProvider.GetRequiredService<IServiceProxyFactory>();
        var service = serviceProvider.GetRequiredService<ScriptingModuleService>();
        return factory.CreateProxy<IScriptingModuleService>(service);
    });


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFluentEmailFactory, YamudFluentEmailFactory>();
builder.Services.AddScoped<RazorViewToStringRenderer>();
builder.Services.AddScoped<IRequestContext, RequestContext>();

builder.Services.AddScoped<UserAccountMask>();

// In your DI container configuration


builder.Services.AddSingleton<EmailConfigurationService>();
builder.Services.AddSingleton(provider =>
{
    var emailConfigurationService = provider.GetRequiredService<EmailConfigurationService>();
    return emailConfigurationService.ConfigureSmtpSender();
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/EmailTemplates/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/EmailTemplates/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
});

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7202")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

});

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
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443; // Set the HTTPS port
});

var app = builder.Build();

app.UseMiddleware<RequestInfoMiddleware>();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowOrigin");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
