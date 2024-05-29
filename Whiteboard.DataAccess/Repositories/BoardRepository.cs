using Microsoft.EntityFrameworkCore;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Models;

namespace Whiteboard.DataAccess.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly BoardContext _boardContext;

        public BoardRepository(BoardContext boardContext)
        {
            _boardContext = boardContext;
        }

        public async Task<Board> Add(Board board)
        {
            board.MemberIds.Add(board.OwnerId);
            await _boardContext.Boards.AddAsync(board);
            await _boardContext.SaveChangesAsync();
            return board;
        }

        public Task<bool> Delete(uint boardId)
        {
            throw new NotImplementedException();
        }

        public async Task<Board?> GetById(uint id)
        {
            var board = await _boardContext.Boards.Where(b => b.Id.Equals(id)).FirstOrDefaultAsync();
            return board;
        }

        public Task<IEnumerable<Board>> GetByMemberId(uint memberId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Board>> GetByOwnerId(uint ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<Board> Update(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
