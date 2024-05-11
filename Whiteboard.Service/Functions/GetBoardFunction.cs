using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Repositories;

namespace Whiteboard.Service.Functions
{
    public class GetBoardFunction(ILogger<GetBoardFunction> logger, IBoardRepository whiteboardRepository)
    {
        private readonly ILogger<GetBoardFunction> _logger = logger;
        private readonly IBoardRepository _whiteboardRepository = whiteboardRepository;


        [Function("GetBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("File uploaded to Azure, processing...");
                var formData = await req.ReadFormAsync();
                var file = req.Form.Files["file"];
                if (file.ContentType != "application/json") return new BadRequestObjectResult("Incorrect file format");
                return new OkObjectResult(file.FileName + " - " + file.Length.ToString() + ", ContentType: " + file.ContentType);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An exception occured: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Function("GetBoardById")]
        public async Task<IActionResult> RunTwo([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            try
            {
                //Guid.Parse("6a0dfaef-a375-494b-b5eb-86c4314870ff")
                var board = await _whiteboardRepository.GetById(1).ConfigureAwait(false);
                return new OkObjectResult(board);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An exception occured: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
