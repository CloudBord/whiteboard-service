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
        Task<Board?> GetBoard(uint boardId, uint memberId);
        Task<IEnumerable<Board>> GetAllBoards(uint userId);
        Task UpdateBoard(Board board);
        Task<bool> DeleteBoard(uint boardId, uint ownerId);
    }
}
