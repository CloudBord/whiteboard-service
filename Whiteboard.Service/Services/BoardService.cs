﻿using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Models;
using Whiteboard.DataAccess.Repositories;

namespace Whiteboard.Service.Services
{
    public class BoardService(ILogger<BoardService> logger, IBoardRepository whiteboardRepository) : IBoardService
    {
        private readonly ILogger<BoardService> _logger = logger;
        private readonly IBoardRepository _whiteboardRepository = whiteboardRepository;

        public async Task<Board> CreateBoard(Guid ownerId, string name)
        {
            return await _whiteboardRepository.Add(
                new Board
                {
                    Name = name,
                    OwnerId = ownerId
                });
        }

        public async Task<bool> DeleteBoard(uint boardId, Guid ownerId)
        {
            return await _whiteboardRepository.DeleteBoard(boardId, ownerId);
        }

        public async Task<IEnumerable<Board>> GetAllBoards(Guid userId)
        {
            return await _whiteboardRepository.GetByMemberId(userId);
        }

        public async Task<Board?> GetBoard(uint boardId, Guid memberId)
        {
            return await _whiteboardRepository.GetByBoardIdAndUserId(boardId, memberId);
        }

        public async Task<Board?> UpdateBoard(uint boardId, Guid userId, string boardContents)
        {
            Board? board = await _whiteboardRepository.GetByBoardIdAndUserId(boardId, userId);
            if (board == null)
            {
                return null;
            }
            
            return await _whiteboardRepository.UpdateBoard(board);
        }
    }
}
