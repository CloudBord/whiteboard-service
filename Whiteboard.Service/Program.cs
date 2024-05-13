using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Repositories;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
        {
            services.AddApplicationInsightsTelemetryWorkerService();
        
            string? ConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings:PostgreSQL");
            string? DatabaseName = Environment.GetEnvironmentVariable("ConnectionStrings:DatabaseName");
            if (ConnectionString == null || DatabaseName == null) throw new InvalidOperationException("No valid connection strings");

            //services.AddDbContext<BoardContext>(
                //options =>
                //{
                //    options.UseCosmos(
                //        "https://localhost:8081/",
                //        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                //        databaseName: "BoardsDB",
                //        options =>
                //        {
                //            options.RequestTimeout(TimeSpan.FromMinutes(1));
                //        }
                //    );
                //    options.EnableSensitiveDataLogging();
                //}
                //); 
        
            services.AddDbContext<BoardContext>();
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.ConfigureFunctionsApplicationInsights();
        })
    .Build();

host.Run();
