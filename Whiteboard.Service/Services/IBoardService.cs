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
        Task<Board> CreateBoard(Guid ownerId, string name);
        Task<Board?> GetBoard(uint boardId, Guid memberId);
        Task<IEnumerable<Board>> GetAllBoards(Guid userId);
        Task<Board?> UpdateBoard(uint boardId, Guid userId, string boardContents);
        Task<bool> DeleteBoard(uint boardId, Guid ownerId);
    }
}
