using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();

        services.AddDbContext<BoardContext>();
        services.AddScoped<IBoardRepository, BoardRepository>();

        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
