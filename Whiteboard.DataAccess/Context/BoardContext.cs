using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Whiteboard.DataAccess.Models;

namespace Whiteboard.DataAccess.Context
{
    public class BoardContext : DbContext, IBoardContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Board> Boards { get; set; }

        public BoardContext(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(
                _configuration["ConnectionString-BoardsDB"] ?? _configuration.GetConnectionString("Npgsql"),
                options =>
                {
                    options.EnableRetryOnFailure(5, TimeSpan.FromSeconds(15), null);
                });
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
