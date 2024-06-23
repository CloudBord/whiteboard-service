using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Whiteboard.DataAccess.Models;
using Whiteboard.DataAccess.Repositories;
using Whiteboard.Service.DTO;
using Whiteboard.Service.Models;
using Whiteboard.Service.Request;
using Whiteboard.Service.Services;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace Whiteboard.Service.Functions
{
    public class CreateBoardFunction(ILogger<CreateBoardFunction> logger, IMapper mapper, IBoardRepository boardRepository, IClaimsHandler claimsHandler, IMessageService messageService)
    {
        private readonly ILogger _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IBoardRepository _boardRepository = boardRepository;
        private readonly IClaimsHandler _claimsHandler = claimsHandler;
        private readonly IMessageService _messageService = messageService;

        [Function("CreateBoard")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "boards")] [FromBody] CreateBoardRequest req, FunctionContext functionContext)
        {
            try
            {
                Guid ownerId = _claimsHandler.GetUserId(functionContext);

                if (req == null || req.Title == null || req.Title.Trim() == string.Empty)
                {
                    return new BadRequestObjectResult("Title is invalid");
                }

                Board board = new Board
                {
                    Name = req.Title,
                    OwnerId = ownerId,
                    MemberIds = [ownerId]
                };
                await _boardRepository.Add(board);
                BoardDTO boardDTO = _mapper.Map<BoardDTO>(board);
                DocumentDTO documentDTO = _mapper.Map<DocumentDTO>(board);
                _messageService.TrySendMessage<DocumentDTO>("board-create", documentDTO, out bool result);
                return new OkObjectResult(boardDTO);
            }
            catch (Exception ex)
            {
                // I miss the elegance of Java exception handling
                if(ex is UnauthorizedAccessException)
                {
                    _logger.LogWarning("Unauthorized access attempt detected: " + ex.Message);
                    return new BadRequestResult();
                }
                if(ex is ArgumentNullException || ex is FormatException || ex is OverflowException)
                {
                    _logger.LogWarning("Unauthorized access attempt detected with invalid Guid: " + ex.Message);
                    return new BadRequestResult();
                }
                _logger.LogWarning("An exception has occurred: " + ex.Message);
                return new BadRequestResult();
            }
        }
    }
} 
