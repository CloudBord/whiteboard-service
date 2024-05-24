using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Repositories;

namespace Whiteboard.Service.Functions
{
    public class CreateBoardFunction(ILogger<CreateBoardFunction> logger, IBoardRepository whiteboardRepository)
    {
        private readonly ILogger<CreateBoardFunction> _logger = logger;
        private readonly IBoardRepository _whiteboardRepository = whiteboardRepository;

        [Function("CreateBoardFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
