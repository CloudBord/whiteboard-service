using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Whiteboard.Service.Models;
using Whiteboard.Service.Request;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class CreateBoardFunction(ILogger<SaveBoardFunction> logger, IBoardService boardService, IMapper mapper)
    {
        private readonly ILogger<SaveBoardFunction> _logger = logger;
        private readonly IBoardService _boardService = boardService;
        private readonly IMapper _mapper = mapper;

        [Function("CreateBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "boards/create")] HttpRequestData req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var createBoardRequest = JsonSerializer.Deserialize<CreateBoardRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var board = await _boardService.CreateBoard(1, createBoardRequest.Name);
            BoardDTO boardDto = _mapper.Map<BoardDTO>(board);
            return new OkObjectResult(boardDto);
        }
    }
}
