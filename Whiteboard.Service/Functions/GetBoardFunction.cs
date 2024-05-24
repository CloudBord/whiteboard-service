using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Repositories;

namespace Whiteboard.Service.Functions
{
    public class GetBoardFunction(ILogger<GetBoardFunction> logger, IBoardRepository whiteboardRepository)
    {
        private readonly ILogger<GetBoardFunction> _logger = logger;
        private readonly IBoardRepository _whiteboardRepository = whiteboardRepository;

        [Function("GetBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "GetBoard/{id}")] HttpRequest req, uint id)
        {
            try
            {
                var board = await _whiteboardRepository.GetById(id).ConfigureAwait(false);
                if (board == null) return new OkObjectResult("No board exists");
                return new OkObjectResult("Here is board with id " + id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An exception occured: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
