using Datalayer.Context;
using Datalayer.Contracts;
using Datalayer.Models;
using Datalayer.Repositories;
using Domain.Parse;
using Domain.Parse.Contracts;
using Infrastructure.Common.Consul;
using Infrastructure.Common.Eventstores;
using Infrastructure.Common.Extensions;
using Infrastructure.Common.Logging;
using Infrastructure.Common.MessageBrokers;
using Infrastructure.Common.Outbox;
using Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ParseService;
using ParseService.Contracts;
using ParseService.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Serilog configuration
builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
                 .ReadFrom.Services(services)
                 .Enrich.FromLogContext()
                 .WriteTo.Console());

// Build configuration
builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Configure services
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStringKeys.App)));

builder.Services.AddDbContext<GlobalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStringKeys.Global)));

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Dependency Injection (DI) setup
builder.Services.AddSingleton<IMongoDbSettings>(sp => sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddScoped<IParseParameterRepository, ParseParameterRepository>();
builder.Services.AddScoped<IParseParameterService, ParseParameterService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<IParser, Parser>();
//builder.Services.AddSingleton<IExcelMongoService<ExcelResult>, ExcelMongoService<ExcelResult>>();
builder.Services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddScoped<IParserMongoService, ParserMongoService>();
//builder.Services.AddScoped<IExcelMongoService, ExcelMongoService>();

// Add infrastructure services
builder.Services.AddConsul(builder.Configuration);
builder.Services.AddMessageBroker(builder.Configuration);
builder.Services.AddEventStore<ParseAggregate>(builder.Configuration,
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStringKeys.EventStore)));
builder.Services.AddOutbox(builder.Configuration,
    options => options.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionStringKeys.Outbox)));
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddCore(typeof(Program), typeof(EventsExtensions), typeof(DatabaseContext));
//builder.Services.AddAutoMapper(typeof(Program));

// Add Controllers
builder.Services.AddControllersWithViews();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Logging setup
app.UseLogging(builder.Configuration, app.Services.GetRequiredService<ILoggerFactory>());

// Swagger setup
app.UseSwagger(builder.Configuration);

// Consul setup
app.UseConsul(app.Lifetime);

// Core services setup
app.UseCore();

// Subscribe to events
app.UseSubscribeAllEvents();

// Update database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    context.Database.Migrate();
}

// Start the application
app.Run();
