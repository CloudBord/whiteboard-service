using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Whiteboard.Service.Models;
using Whiteboard.Service.Requests;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class SaveBoardFunction(ILogger<SaveBoardFunction> logger, IMapper mapper, IValidator<SaveBoardRequest> validator, IBoardService boardService, IClaimsHandler claimsHandler)
    {
        private readonly ILogger _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<SaveBoardRequest> _validator = validator;
        private readonly IBoardService _boardService = boardService;
        private readonly IClaimsHandler _claimsHandler = claimsHandler;

        [Function("SaveBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "boards")] HttpRequest req, FunctionContext functionContext)
        {
            Guid userId = _claimsHandler.GetUserId(functionContext);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var creq = JsonConvert.DeserializeObject<SaveBoardRequest>(requestBody);

            var board = await _boardService.UpdateBoard(creq!.Id, userId, creq!.BoardContents);
            if (board == null)
            {
                return new BadRequestObjectResult("Board does not exist");
            }
            BoardDTO boardDto = _mapper.Map<BoardDTO>(board);


            return new OkObjectResult(boardDto);
        }
    }
}
