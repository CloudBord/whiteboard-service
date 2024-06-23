using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.DataAccess.Models;

namespace Whiteboard.DataAccess.Repositories
{
    public interface IBoardRepository
    {
        Task<Board> Add(Board board);
        Task<Board?> GetBoard(uint boardId, Guid userId);
        Task<IEnumerable<Board>> GetAllBoards(Guid userId);
        Task<Board> UpdateBoard(Board board);
        Task<bool> DeleteBoard(uint boardId, Guid ownerId);
    }
}
