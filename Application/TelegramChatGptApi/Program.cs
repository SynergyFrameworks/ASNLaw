using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Serilog;
using TelegramChatGptApi.Application.Interfaces;
using TelegramChatGptApi.Application.Services;
using TelegramChatGptApi.Infrastructure;
using TelegramChatGptApi.Infrastructure.Context;
using TelegramChatGptApi.Application.DTOs;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();
//var builder = WebApplication.CreateBuilder(args);

//// Add Azure Key Vault to Configuration
//var keyVaultUrl = "https://<Your-KeyVault-Name>.vault.azure.net/";
//var azureCredentials = new DefaultAzureCredential();

//builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), azureCredentials);

//// Load API keys from Key Vault or appsettings.json
//var configuration = builder.Configuration;

//// Proceed with adding services and configuring the app
// Options Pattern
//builder.Services.Configure<TelegramOptions>(builder.Configuration.GetSection("Telegram"));



// Load configuration from appsettings.json
var configuration = builder.Configuration;

// Configure Neo4j
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNeo4j(configuration["Neo4j:ConnectionString"]));


// Configure OpenAI API settings from appsettings.json
builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));



// Configure Swagger and API services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IChatGptService, ChatGptService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Add Infrastructure with configuration from appsettings.json
builder.Services.AddInfrastructure(configuration);

// Build and configure the app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TelegramChatGptApi v1");
        c.RoutePrefix = string.Empty; // Swagger UI at the root
    });
}

app.UseAuthorization();
app.MapControllers();
app.Run();
