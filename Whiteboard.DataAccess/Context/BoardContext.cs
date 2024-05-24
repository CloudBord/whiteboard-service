using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                _configuration["ConnectionStrings:Npgsql"], 
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
