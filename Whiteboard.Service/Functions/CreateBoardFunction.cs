using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Whiteboard.Service.Attributes;
using Whiteboard.Service.Models;
using Whiteboard.Service.Request;
using Whiteboard.Service.Services;

namespace Whiteboard.Service.Functions
{
    public class CreateBoardFunction(ILogger<SaveBoardFunction> logger, IMapper mapper, IValidator<CreateBoardRequest> validator, IBoardService boardService, IClaimsHandler claimsHandler)
    {
        private readonly ILogger _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateBoardRequest> _validator = validator;
        private readonly IBoardService _boardService = boardService;
        private readonly IClaimsHandler _claimsHandler = claimsHandler;

        [Authorized]
        [Function("CreateBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "boards")] HttpRequestData req, FunctionContext functionContext)
        {
            Guid userId = _claimsHandler.GetUserId(functionContext);

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var creq = JsonConvert.DeserializeObject<CreateBoardRequest>(requestBody);

            var board = await _boardService.CreateBoard(userId, creq!.Name);
            BoardDTO boardDto = _mapper.Map<BoardDTO>(board);
            return new OkObjectResult(boardDto);
        }
    }
} 
