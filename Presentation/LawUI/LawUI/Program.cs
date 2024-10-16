using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using LawUI.Components;
using LawUI.Components.Account;
using LawUI.Data;
using LawUI.Domain;
using LawUI.Domain.Model;
using LawUI.Domain.Services;
using LawUI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.CognitiveServices.Speech;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MudBlazor.Services;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using Serilog.Enrichers;
using System.Reflection;
using LawUI;
using LawUI.Components.Stores;
using LawUI.Hubs;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using LawUI.Exceptions;


var builder = WebApplication.CreateBuilder(args);


// Configure Serilog
ConfigureLogging();
builder.Host.UseSerilog();

builder.Services.AddSingleton(sp =>
{
    var cognitiveServicesConfig = sp.GetRequiredService<IConfiguration>().GetSection("AzureCognitiveServices");
    var subscriptionKey = cognitiveServicesConfig["SubscriptionKey"];
    var endpoint = cognitiveServicesConfig["Endpoint"];

    if (string.IsNullOrEmpty(subscriptionKey) || string.IsNullOrEmpty(endpoint))
    {
        throw new AzureConfigurationException("Azure Cognitive Services are not properly configured. Please check your 'AzureCognitiveServices' settings in the configuration.");
    }

    return new ComputerVisionClient(new ApiKeyServiceClientCredentials(subscriptionKey))
    {
        Endpoint = endpoint
    };
});

// Add MudBlazor services
builder.Services.AddMudServices();
builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddSignalR();

// Example of using SerilogRequestLogging
builder.Logging.ClearProviders(); // Clear other log providers
builder.Logging.AddSerilog(); // Add Serilog to logging


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Add services to the container.
builder.Services.Configure<AzureAIOptions>(builder.Configuration.GetSection("AzureAI"));
builder.Services.AddSingleton<DocumentAnalysisClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<AzureAIOptions>>().Value;
    return new DocumentAnalysisClient(new Uri(options.Endpoint), new AzureKeyCredential(options.ApiKey));
});

// Read Azure Cognitive Services settings
var azureSettings = builder.Configuration.GetSection("AzureCognitiveServices").Get<AzureCognitiveServicesSettings>();

// Register SpeechRecognizer as a singleton
builder.Services.AddSingleton(sp =>
{
    var config = SpeechConfig.FromSubscription(azureSettings.SubscriptionKey, azureSettings.Region);
    return new SpeechRecognizer(config);
});


builder.Services.AddSingleton(sp =>
{
    var formRecognizerConfig = sp.GetRequiredService<IConfiguration>().GetSection("AzureFormRecognizer");
    var endpoint = formRecognizerConfig["Endpoint"];
    var apiKey = formRecognizerConfig["ApiKey"];

    if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(apiKey))
    {
        throw new AzureConfigurationException("Azure Form Recognizer is not properly configured. Please check your 'AzureFormRecognizer' settings in the configuration.");
    }

    return new DocumentAnalysisClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
});

// Register memory cache
builder.Services.AddMemoryCache();


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();
builder.Services.AddScoped<SpeechToTextViewModel>();
builder.Services.AddScoped<MongoDbService>();
builder.Services.AddScoped<UserClaimService>();
builder.Services.AddScoped<IAdvancedTextExtractorService, AdvancedTextExtractorService>();
builder.Services.AddScoped<IDocumentCacheService, DocumentCacheService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://your-api-base-url/") });
builder.Services.AddScoped<IThemeService, ThemeService>();


builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// Configure MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection") ?? throw new InvalidOperationException("Connection string 'MongoDbConnection' not found.");
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongoConnectionString));
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("YourDatabaseName");
});


builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddUserStore<CustomUserStore>() // Add this line
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();


app.MapHub<ChatHub>("/chathub");
app.MapControllers();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


/// 
app.UseGlobalErrorHandling();
app.UseSerilogRequestLogging(); // Middleware to log request information


app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "text/html";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var errorMessage = exception switch
        {
            AzureConfigurationException => $"<h1>Azure Configuration Error</h1><p>{exception.Message}</p>",
            ArgumentException => $"<h1>Invalid Argument</h1><p>{exception.Message}</p>",
            FileNotFoundException => $"<h1>File Not Found</h1><p>{exception.Message}</p>",
            UnauthorizedAccessException => $"<h1>Unauthorized Access</h1><p>{exception.Message}</p>",
            _ => "<h1>An unexpected error occurred while processing your request.</h1>"
        };

        await context.Response.WriteAsync(errorMessage);
    });
});




app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(LawUI.Client.UserInfo).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();


void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .Enrich.WithMachineName() // This should work without explicit namespace import
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}