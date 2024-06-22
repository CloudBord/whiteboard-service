using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.Service.DTO;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class DeleteBoardFunction(ILogger<DeleteBoardFunction> logger, IBoardService boardService, IClaimsHandler claimsHandler)
    {
        private readonly ILogger _logger = logger;
        private readonly IBoardService _boardService = boardService;
        private readonly IClaimsHandler _claimsHandler = claimsHandler;

        [Function("DeleteBoard")]
        [RabbitMQOutput(ConnectionStringSetting = "ConnectionStrings:RabbitMQ", QueueName = "board-delete")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "boards/{id}")] HttpRequest req, FunctionContext functionContext, uint id)
        {
            try
            {
                Guid userId = _claimsHandler.GetUserId(functionContext);
                var result = await _boardService.DeleteBoard(id, userId);
                return new OkObjectResult(new ResultDTO { Result = result, BoardId = id });
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An exception occured: " + ex.Message);
                return new BadRequestObjectResult(new ResultDTO { Result = false, BoardId = 0 });
            }
        }
    }
}
