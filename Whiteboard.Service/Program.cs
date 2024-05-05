using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whiteboard.DataAccess;
using Whiteboard.DataAccess.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        string? ConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:CosmosDB");
        if (ConnectionString == null) throw new InvalidOperationException("No valid ");

        services.AddDbContext<CosmosContext>(options =>
            options.UseCosmos(
                    ConnectionString,
                    "WhiteboardDB"
                )
        );
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddSingleton<IBoardRepository, BoardRepository>();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
