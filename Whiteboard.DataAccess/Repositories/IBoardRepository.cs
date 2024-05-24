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
        Task Add(Board board);
        Task<Board?> GetById(uint id);
        Task<IEnumerable<Board>> GetByMemberId(uint memberId);
        Task<IEnumerable<Board>> GetByOwnerId(uint ownerId);
        Task<Board> Update(Board board);
        Task<bool> Delete(uint boardId);
    }
}
