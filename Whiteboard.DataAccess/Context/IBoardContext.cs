using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.DataAccess.Models;

namespace Whiteboard.DataAccess.Context
{
    public interface IBoardContext : IDisposable
    {
        DbSet<Board> Boards { get; set; }
    }
}
