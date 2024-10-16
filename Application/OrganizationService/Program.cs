using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Infrastruture.Caching;
using Datalayer.Context;
using Datalayer.Domain.Demographics;
using Datalayer.Domain.Group;
using Datalayer.Domain;
using Datalayer.Domain.Storage;
using Infrastructure.CQRS.Contracts;
using Infrastructure.CQRS.Handlers;
using Infrastructure.Security;
using Infrastructure.Common.Events;
using Infrastructure.Common.Logging;
using Infrastructure.Common.MessageBrokers;
using Infrastructure.Common.Outbox;
using Infrastructure.Common.Sorting;
using Infrastructure.Swagger;
using Serilog;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using OrganizationService.Model;
using OrganizationService.Providers;
using Microsoft.Extensions.DependencyInjection;
using Datalayer.Models;
using Infrastructure.CQRS.Models;
using Organization.Services;
using Infrastruture.Common.Caching;
using Microsoft.Extensions.Configuration;
using Infrastructure.Common.Extensions;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog logging
builder.Host.UseSerilog((ctx, lc) => lc
    .ReadFrom.Configuration(ctx.Configuration));

// Load configuration from appsettings and environment variables
var configuration = builder.Configuration;
var environment = builder.Environment;

// Add services to the container
builder.Services.AddDbContext<ASNDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString(ConnectionStringKeys.App)));

builder.Services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

//builder.Services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>((options) =>
//    configuration.GetSection("Redis").Get<RedisConfiguration>());

builder.Services.AddScoped<ICrudHandler<ASNDbContext>, CrudHandler>();
builder.Services.AddScoped<IQueryHandler<ASNDbContext>, QueryHandler>();

// Register application services
builder.Services.AddScoped<IService<ASNClient, DefaultSearch<ClientSearchResult>>, ClientService>();
builder.Services.AddScoped<IService<DocumentSource, DefaultSearch<DocumentSource>>, DocumentSourceService>();
builder.Services.AddScoped<IService<ASNGroup, DefaultSearch<GroupSearchResult>>, GroupService>();
builder.Services.AddScoped<IService<Module, DefaultSearch<ModuleSearchResult>>, ModuleService>();
//builder.Services.AddScoped<IService<OrganizationOuput, DefaultSearch<OrganizationSearchResult>>, OrganizationService>();
//... more scoped services

builder.Services.AddScoped<ISortingOption, SortOption>();
builder.Services.AddScoped<IEventBus, EventBus>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

// Add infrastructure services
builder.Services.AddMessageBroker(configuration);
builder.Services.AddOutbox(configuration, options =>
    options.UseSqlServer(configuration.GetConnectionString(ConnectionStringKeys.Outbox)));

builder.Services.AddSwagger(configuration);
builder.Services.AddCaching(configuration);
builder.Services.AddCore(typeof(Program), typeof(EventsExtensions), typeof(DatabaseContext));
//builder.Services.AddAutoMapper(typeof(Program));

// Authentication
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        var identitySettings = configuration.GetIdentitySettings();
//        options.Authority = identitySettings.Authority;
//        options.Audience = identitySettings.Audience;

//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateLifetime = true,
//            ValidateAudience = false,
//            ValidAudience = identitySettings.Audience,
//            ValidIssuer = identitySettings.ValidIssuer
//        };

//        if (environment.IsEnvironment("docker"))
//        {
//            options.BackchannelHttpHandler = new HttpClientHandler
//            {
//                ServerCertificateCustomValidationCallback = delegate { return true; }
//            };
//        }
//    });

// Role-based Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "api.organization");
    });
    // Additional role-based policies
    options.AddPolicy("OnlySuperAdminAccess", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("superAdmin");
    });
    options.AddPolicy("AdminAccess", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("superAdmin", "admin");
    });
    // Add other policies...
});

//builder.Services.AddPermissions<InternalPermissionsProvider>()
//    .AddAuthorizationPermission();

// Add controllers
builder.Services.AddControllersWithViews();

// Build the app
var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();  // Use Swagger
///app.UseLogging(configuration);  // Use custom logging
app.UseSubscribeAllEvents();  // Use custom event subscriptions

app.UseCore();  // Core application logic

app.Run();



