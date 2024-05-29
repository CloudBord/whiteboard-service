using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class SaveBoardFunction(ILogger<SaveBoardFunction> logger, IBoardService boardService)
    {
        private readonly ILogger<SaveBoardFunction> _logger = logger;
        private readonly IBoardService _boardService = boardService;

        [Function("SaveBoard")]
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
    }
}
