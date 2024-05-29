using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Models;
using Whiteboard.DataAccess.Repositories;

namespace Whiteboard.Service.Services
{
    public class BoardService(ILogger<BoardService> logger, IBoardRepository whiteboardRepository) : IBoardService
    {
        private readonly ILogger<BoardService> _logger = logger;
        private readonly IBoardRepository _whiteboardRepository = whiteboardRepository;

        public async Task<Board> CreateBoard(uint ownerId, string name)
        {
            return await _whiteboardRepository.Add(
                new Board
                {
                    Name = name,
                    OwnerId = ownerId
                });
        }

        public async Task<bool> DeleteBoard(uint boardId, uint ownerId)
        {
            return await _whiteboardRepository.Delete(boardId, ownerId);
        }

        public async Task<ICollection<Board>> GetAllBoards(uint userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Board?> GetBoard(uint boardId, uint memberId)
        {
            return await _whiteboardRepository.GetByBoardIdAndUserId(boardId, memberId);
        }

        public async Task UpdateBoard(Board board)
        {
            //Board board = await _whiteboardRepository.GetByBoardIdAndUserId(board.)
            throw new NotImplementedException();
        }
    }
}
