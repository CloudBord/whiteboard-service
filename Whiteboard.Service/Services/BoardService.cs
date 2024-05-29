using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.DataAccess.Models;
using Whiteboard.DataAccess.Repositories;
using Whiteboard.Service.Functions;
using Whiteboard.Service.Models;

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

        public async Task DeleteBoard(Board board)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Board>> GetAllBoards(uint userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Board?> GetById(uint id)
        {
            return await _whiteboardRepository.GetById(id);
        }

        public async Task UdateBoard(Board board)
        {
            throw new NotImplementedException();
        }
    }
}
