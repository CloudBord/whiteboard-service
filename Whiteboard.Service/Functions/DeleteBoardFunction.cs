using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Models;
using Whiteboard.DataAccess.Repositories;
using Whiteboard.Service.DTO;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class DeleteBoardFunction(ILogger<DeleteBoardFunction> logger, IBoardRepository boardRepository, IClaimsHandler claimsHandler)
    {
        private readonly ILogger _logger = logger;
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IClaimsHandler _claimsHandler = claimsHandler;

        [Function("DeleteBoard")]
        [RabbitMQOutput(ConnectionStringSetting = "ConnectionStrings:RabbitMQ", QueueName = "board-delete")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "boards/{boardId}")] HttpRequest req, FunctionContext functionContext, uint boardId)
        {
            try
            {
                Guid userId = _claimsHandler.GetUserId(functionContext);

                var board = await _boardRepository.GetBoard(boardId, userId);
                bool result = (board == null)
                    ? throw new UnauthorizedAccessException("Unauthorized deletion attempt detected")
                    : await _boardRepository.DeleteBoard(boardId, userId);

                return new OkObjectResult(new ResultDTO { Result = result, BoardId = boardId });
            }
            catch(UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An unexpected exception occured: " + ex.Message);
            }
            return new BadRequestObjectResult(new ResultDTO { Result = false, BoardId = 0 });
        }
    }
}
