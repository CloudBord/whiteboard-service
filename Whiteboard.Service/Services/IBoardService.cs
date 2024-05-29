using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.DataAccess.Models;
using Whiteboard.Service.Models;

namespace Whiteboard.Service.Services
{
    public interface IBoardService
    {
        Task<Board> CreateBoard(uint ownerId, string name);
        Task<Board?> GetById(uint id);
        Task<ICollection<Board>> GetAllBoards(uint userId);
        Task UdateBoard(Board board);
        Task DeleteBoard(Board board);
    }
}
