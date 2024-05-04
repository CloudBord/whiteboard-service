using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Repositories;

namespace Whiteboard.Service
{
    public class WhiteboardService(ILogger<WhiteboardService> logger, IWhiteboardRepository whiteboardRepository)
    {
        private readonly ILogger<WhiteboardService> _logger = logger;
        private readonly IWhiteboardRepository _whiteboardRepository = whiteboardRepository;

        [Function("SaveBoard")]
        public async Task<IActionResult> SaveBoard([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("File uploaded to Azure, processing...");
                var formData = await req.ReadFormAsync();
                var file = req.Form.Files["file"];
                if (file.ContentType != "application/json") return new BadRequestObjectResult("Incorrect file format");
                _whiteboardRepository.ToString();
                return new OkObjectResult(file.FileName + " - " + file.Length.ToString() + ", ContentType: " + file.ContentType);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An exception occured: " + ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Function("GetBoard")]
        public async Task<IActionResult> GetBoard([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
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
