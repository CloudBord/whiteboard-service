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
            await _boardContext.Boards.AddAsync(board);
            await _boardContext.SaveChangesAsync();
            return board;
        }

        public async Task<bool> DeleteBoard(uint boardId, Guid ownerId)
        {
            Board board = await _boardContext.Boards.Where(b => b.Id.Equals(boardId) && b.OwnerId.Equals(ownerId)).FirstAsync();
            _boardContext.Boards.Remove(board);
            return await _boardContext.SaveChangesAsync() > 0;
        }

        public async Task<Board?> GetBoard(uint boardId, Guid userId)
        {
            var board = await _boardContext.Boards.Where(b => b.Id.Equals(boardId) && b.MemberIds.Contains(userId)).FirstOrDefaultAsync();
            return board;
        }

        public async Task<IEnumerable<Board>> GetAllBoards(Guid memberId)
        {
            var boards = await _boardContext.Boards.Where(b => b.MemberIds.Contains(memberId)).ToArrayAsync();
            return boards;
        }

        public Task<IEnumerable<Board>> GetByOwnerId(Guid ownerId)
        {
            throw new NotImplementedException();
        }

        public async Task<Board> UpdateBoard(Board board)
        {
            _boardContext.Update(board);
            await _boardContext.SaveChangesAsync();
            return board;
        }
    }
}
