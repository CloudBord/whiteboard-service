using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Repositories;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class DeleteBoardFunction(ILogger<DeleteBoardFunction> logger, IBoardService boardService)
    {
        private readonly ILogger _logger = logger;
        private readonly IBoardService _boardService = boardService;

        [Function("DeleteBoard")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "boards/{id}")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
