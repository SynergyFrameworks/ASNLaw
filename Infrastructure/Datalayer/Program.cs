using Datalayer.Context;
using Datalayer.Extensions;
using Datalayer.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Host.UseSerilog();

// Add dependencies using the extension method
builder.Services.AddDependencies(builder.Configuration);

var app = builder.Build();

try
{
    var seed = args.Contains("/seed");
    if (seed)
    {
        args = args.Except(new[] { "/seed" }).ToArray();

        Log.Information("Seeding database...");

        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ASNDbContext>();
        context.Database.Migrate();
        context.EnsureOrganizationSeeded();
        context.EnsureStorageProvidersSeeded();
        context.EnsureDocumentSeeded();
        context.EnsurePermissionsSeeded();
        context.EnsureUserGroupsSeeded();


        Log.Information("Done seeding database.");
    }
    else
    {
        Log.Information("Starting host...");
        app.Run();
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}
finally
{
    Log.CloseAndFlush();
}
