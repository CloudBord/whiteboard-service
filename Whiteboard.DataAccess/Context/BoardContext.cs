using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Whiteboard.DataAccess.Models;

namespace Whiteboard.DataAccess.Context
{
    public class BoardContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public virtual DbSet<Board> Boards { get; set; }

        // Workaround for mocking with Moq as Moq requires parameterless constructors
        protected BoardContext() : this(null) { }

        public BoardContext(IConfiguration? configuration)
        {
            _configuration = configuration ?? new ConfigurationBuilder().AddUserSecrets(Assembly.GetExecutingAssembly(), true).AddEnvironmentVariables().Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(
                _configuration["ConnectionString-BoardsDB"] ?? _configuration["ConnectionStrings:Npgsql"],
                options =>
                {
                    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(15), null);
                });
        }
    }
}
