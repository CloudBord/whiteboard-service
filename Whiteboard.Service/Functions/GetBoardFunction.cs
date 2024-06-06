using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.Service.Models;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class GetBoardFunction(ILogger<GetBoardFunction> logger, IBoardService boardService, IMapper mapper)
    {
        private readonly ILogger _logger = logger;
        private readonly IBoardService _boardService = boardService;
        private readonly IMapper _mapper = mapper;

        [Function("GetBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "boards/{id}")] HttpRequest req, uint id)
        {
            try
            {
                var board = await _boardService.GetBoard(id, 1);
                BoardDTO boardDto = _mapper.Map<BoardDTO>(board);
                return new OkObjectResult(boardDto);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An exception occured: " + ex.Message);
                return new BadRequestObjectResult("Bad things have happened!");
            }
        }
    }
}
