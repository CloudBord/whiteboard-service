using Azure.Identity;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Repositories;
using Whiteboard.Service.Mapping;
using Whiteboard.Service.Middleware;
using Whiteboard.Service.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder =>
    {
        builder.UseMiddleware<AuthorizationFunctionMiddleware>();
    })
    .ConfigureAppConfiguration((context, builder) =>
    {
        var configuration = builder
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("settings.json", true, true)
            .AddJsonFile("local.settings.json", true, false)
            .AddEnvironmentVariables()
            .Build();

        if (context.HostingEnvironment.IsDevelopment() && !string.IsNullOrEmpty(context.HostingEnvironment.ApplicationName))
        {
            builder.AddUserSecrets(Assembly.GetExecutingAssembly(), true);
        }

        var keyVaultUrl = builder.Build()["KeyVault:Url"];
        if (!context.HostingEnvironment.IsDevelopment() && !string.IsNullOrEmpty(keyVaultUrl))
        {
            var keyVaultUri = new Uri(keyVaultUrl);
            var creds = new DefaultAzureCredential();
            builder.AddAzureKeyVault(keyVaultUri, creds);
        }
    })
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();

        services.AddHttpClient();

        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddDbContext<BoardContext>();
        services.AddSingleton<IJwtHandler, KeycloakJwtHandler>();

        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
