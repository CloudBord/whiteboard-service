using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Whiteboard.Service.Attributes;
using Whiteboard.Service.Models;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class GetBoardFunction(ILogger<GetBoardFunction> logger, IMapper mapper, IBoardService boardService, IClaimsHandler claimsHandler)
    {
        private readonly ILogger _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IBoardService _boardService = boardService;
        private readonly IClaimsHandler _claimsHandler = claimsHandler;

        [Authorized]
        [Function("GetBoard")]
        public async Task<IActionResult> GetBoard([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "boards/{id}")] HttpRequest req, uint id, FunctionContext executionContext)
        {
            try
            {
                Guid userId = _claimsHandler.GetUserId(executionContext);
                var board = await _boardService.GetBoard(id, userId);
                BoardDTO boardDto = _mapper.Map<BoardDTO>(board);
                return new OkObjectResult(boardDto);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An exception occured: " + ex.Message);
                return new BadRequestObjectResult("Bad things have happened!");
            }
        }

        [Authorized]
        [Function("GetBoards")]
        public async Task<IActionResult> GetBoards([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "boards")] HttpRequest req, FunctionContext executionContext)
        {
            try
            {
                Guid userId = _claimsHandler.GetUserId(executionContext);
                var boards = await _boardService.GetAllBoards(userId);
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
