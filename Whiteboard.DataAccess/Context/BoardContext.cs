using Microsoft.Azure.Cosmos.Core;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<Board> Boards { get; set; }

        //public BoardContext() { }

        public BoardContext(DbContextOptions<BoardContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseCosmos(
        //        "https://localhost:8081",
        //        "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
        //        databaseName: "BoardsDB");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("BoardContainer");

            modelBuilder.Entity<Board>()
                .HasNoDiscriminator()
                .HasPartitionKey(b => b.OwnerId)
                .HasKey(b => b.Id);
        }
    }
}
