using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Repositories;
using Whiteboard.Service.Models;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class GetBoardFunction(ILogger<GetBoardFunction> logger, IMapper mapper, IBoardRepository boardRepository, IClaimsHandler claimsHandler)
    {
        private readonly ILogger _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IClaimsHandler _claimsHandler = claimsHandler;

        [Function("GetBoard")]
        public async Task<IActionResult> GetBoard([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "boards/{boardId}")] HttpRequest req, uint boardId, FunctionContext executionContext)
        {
            try
            {
                Guid userId = _claimsHandler.GetUserId(executionContext);
                var board = await _boardRepository.GetBoard(boardId, userId);
                BoardDTO boardDto = _mapper.Map<BoardDTO>(board);
                return new OkObjectResult(boardDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An exception occured: " + ex.Message);
                return new BadRequestObjectResult("Bad things have happened!");
            }
        }

        [Function("GetBoards")]
        public async Task<IActionResult> GetBoards([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "boards")] HttpRequest req, FunctionContext executionContext)
        {
            try
            {
                Guid userId = _claimsHandler.GetUserId(executionContext);
                var boards = await _boardRepository.GetAllBoards(userId);
                BoardDTO[] boardsDto = _mapper.Map<BoardDTO[]>(boards); ;
                return new OkObjectResult(boardsDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An exception occured: " + ex.Message);
                return new BadRequestObjectResult("Bad things have happened!");
            }
        }
    }
}
