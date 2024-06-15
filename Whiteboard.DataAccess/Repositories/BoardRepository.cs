using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Models;

namespace Whiteboard.DataAccess.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly BoardContext _boardContext;
        private readonly ILogger _logger;

        public BoardRepository(BoardContext boardContext, ILogger<BoardRepository> logger)
        {
            _boardContext = boardContext;
            _logger = logger;
        }

        public async Task<Board> Add(Board board)
        {
            board.MemberIds.Add(board.OwnerId);
            await _boardContext.Boards.AddAsync(board);
            await _boardContext.SaveChangesAsync();
            return board;
        }

        public async Task<bool> Delete(uint boardId, uint ownerId)
        {
            var board = await _boardContext.Boards.Where(b => b.Id.Equals(boardId) && b.OwnerId.Equals(ownerId)).FirstOrDefaultAsync();
            
            if (board == null)
            {
                _logger.LogWarning("Unauthorized deletion attempt detected");
                return false;
            }
            _boardContext.Remove(board);
            return await _boardContext.SaveChangesAsync() > 0;
        }

        public async Task<Board?> GetByBoardIdAndUserId(uint boardId, uint userId)
        {
            var board = await _boardContext.Boards.Where(b => b.Id.Equals(userId) && b.MemberIds.Contains(userId)).FirstOrDefaultAsync();
            return board;
        }

        public async Task<IEnumerable<Board>> GetByMemberId(uint memberId)
        {
            var boards = await _boardContext.Boards.Where(b => b.MemberIds.Contains(memberId)).ToArrayAsync();
            return boards;
        }

        public Task<IEnumerable<Board>> GetByOwnerId(uint ownerId)
        {
            throw new NotImplementedException();
        }

        public Task<Board> Update(Board board, uint memberId)
        {
            throw new NotImplementedException();
        }
    }
}
