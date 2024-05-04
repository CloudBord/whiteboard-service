using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whiteboard.DataAccess.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.AddScoped<IWhiteboardRepository, WhiteboardRepository>();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
