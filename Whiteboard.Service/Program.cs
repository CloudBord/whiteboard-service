using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Repositories;
using Microsoft.Azure.Cosmos;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        string? ConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:CosmosDB");
        string? DatabaseName = Environment.GetEnvironmentVariable("ConnectionStrings:DatabaseName");
        if (ConnectionString == null || DatabaseName == null) throw new InvalidOperationException("No valid connection strings");

        services.AddDbContextFactory<BoardContext>(options =>
            options.UseCosmos(
                    connectionString: ConnectionString,
                    databaseName: DatabaseName,
                    options =>
                    {
                        options.ConnectionMode(ConnectionMode.Gateway);
                        options.RequestTimeout(TimeSpan.FromMinutes(1));
                    }
                )
        );

        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddScoped<BoardContext>();
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
