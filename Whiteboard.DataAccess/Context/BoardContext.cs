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

        public BoardContext() { }

        public BoardContext(DbContextOptions<BoardContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
