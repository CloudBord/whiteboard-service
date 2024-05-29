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
        Task<Board?> GetByBoardIdAndUserId(uint boardId, uint userId);
        Task<IEnumerable<Board>> GetByMemberId(uint userId);
        Task<IEnumerable<Board>> GetByOwnerId(uint userId);
        Task<Board> Update(Board board, uint memberId);
        Task<bool> Delete(uint boardId, uint ownerId);
    }
}
