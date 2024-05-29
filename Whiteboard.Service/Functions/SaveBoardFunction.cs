using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;
using Whiteboard.Service.Request;
using Whiteboard.Service.Requests;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class SaveBoardFunction(ILogger<SaveBoardFunction> logger, IBoardService boardService)
    {
        private readonly ILogger<SaveBoardFunction> _logger = logger;
        private readonly IBoardService _boardService = boardService;

        [Function("SaveBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequest req)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var saveBoardRequest = JsonSerializer.Deserialize<SaveBoardRequest>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return new OkObjectResult("");
        }
    }
}
