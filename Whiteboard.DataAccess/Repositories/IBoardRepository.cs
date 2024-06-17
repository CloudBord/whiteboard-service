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
        Task<Board?> GetByBoardIdAndUserId(uint boardId, Guid userId);
        Task<IEnumerable<Board>> GetByMemberId(Guid userId);
        Task<IEnumerable<Board>> GetByOwnerId(Guid userId);
        Task<Board> Update(Board board, Guid memberId);
        Task<bool> Delete(uint boardId, Guid ownerId);
    }
}
