using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Moq.EntityFrameworkCore;
using System.Security.Claims;
using Whiteboard.DataAccess.Context;
using Whiteboard.DataAccess.Models;
using Whiteboard.DataAccess.Repositories;
using Whiteboard.Service.Functions;
using Whiteboard.Service.Mapping;
using Whiteboard.Service.Models;
using Whiteboard.Service.Request;
using Whiteboard.Service.Services;

namespace Whiteboard.Tests.Integration
{
    public class CreateBoardFunctionTest
    {
        private readonly ILogger<CreateBoardFunction> _loggerMock;
        private readonly IMapper _mapper;
        private readonly Mock<BoardContext> _contextMock;
        private readonly IBoardRepository _boardRepository;
        private readonly IClaimsHandler _claimsHandler;
        private readonly Mock<IMessageService> _messageServiceMock;

        private readonly Mock<FunctionContext> _functionContextMock;
        private readonly CreateBoardFunction _createBoardFunction;

        public CreateBoardFunctionTest()
        {
            _loggerMock = NullLogger<CreateBoardFunction>.Instance;
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile())));
            _contextMock = new Mock<BoardContext>();
            _boardRepository = new BoardRepository(_contextMock.Object);
            _claimsHandler = new ClaimsHandler();
            _messageServiceMock = new Mock<IMessageService>();

            _functionContextMock = new Mock<FunctionContext>();
            _createBoardFunction = new CreateBoardFunction(_loggerMock, _mapper, _boardRepository, _claimsHandler, _messageServiceMock.Object);
        }

        //https://github.com/Azure/azure-functions-dotnet-worker/issues/281#issuecomment-1926856798

        [Fact]
        public async Task CreateBoardFunctionTest_createBoard_withValidBody()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            new List<Claim>
                            {
                                new( "sub", Guid.NewGuid().ToString())
                            }
                        }
                    }
                );

            _contextMock.Setup<DbSet<Board>>(x => x.Boards)
                .ReturnsDbSet([]);

            string expectedTitle = "Test";
            CreateBoardRequest request = new CreateBoardRequest
            {
                Title = expectedTitle,
            };

            // act
            var result = await _createBoardFunction.Run(request, _functionContextMock.Object);

            // assert
            var httpResult = Assert.IsAssignableFrom<OkObjectResult>(result);
            var httpBody = httpResult.Value as BoardDTO;
            Assert.NotNull(httpBody);
            Assert.Equal(expectedTitle, httpBody.Name);
        }

        [Fact]
        public async Task CreateBoardFunctionTest_createBoard_withInvalidClaims_invalidGuid()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            new List<Claim>
                            {
                                new("sub", "invalid-guid")
                            }
                        }
                    }
                );

            CreateBoardRequest request = new CreateBoardRequest
            {
                Title = "Test",
            };

            // act
            var result = await _createBoardFunction.Run(request, _functionContextMock.Object);

            // assert
            var httpResult = Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task CreateBoardFunctionTest_createBoard_withInvalidClaims_emptyGuid()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            new List<Claim>
                            {
                                new("sub", string.Empty)
                            }
                        }
                    }
                );

            CreateBoardRequest request = new CreateBoardRequest
            {
                Title = "Test",
            };

            // act
            var result = await _createBoardFunction.Run(request, _functionContextMock.Object);

            // assert
            var httpResult = Assert.IsAssignableFrom<BadRequestResult>(result);
        }

        [Fact]
        public async Task CreateBoardFunctionTest_createBoard_withInvalidBody_titleIsNull()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            new List<Claim>
                            {
                                new( "sub", Guid.NewGuid().ToString())
                            }
                        }
                    }
                );

            CreateBoardRequest request = new CreateBoardRequest
            {
                Title = null,
            };

            // act
            var result = await _createBoardFunction.Run(request, _functionContextMock.Object);

            // assert
            Assert.IsAssignableFrom<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task CreateBoardFunctionTest_createBoard_withInvalidBody_titleIsEmpty()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            new List<Claim>
                            {
                                new("sub", Guid.NewGuid().ToString())
                            }
                        }
                    }
                );

            CreateBoardRequest request = new CreateBoardRequest
            {
                Title = string.Empty,
            };

            // act
            var result = await _createBoardFunction.Run(request, _functionContextMock.Object);

            // assert
            var httpResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.IsType<string>(httpResult.Value as string);
        }

        [Fact]
        public async Task CreateBoardFunctionTest_createBoard_withInvalidBody_titleIsTrimmableCharacters()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            new List<Claim>
                            {
                                new("sub", Guid.NewGuid().ToString())
                            }
                        }
                    }
                );

            CreateBoardRequest request = new CreateBoardRequest
            {
                Title = "    \r  \n",
            };

            // act
            var result = await _createBoardFunction.Run(request, _functionContextMock.Object);

            // assert
            var httpResult = Assert.IsAssignableFrom<BadRequestObjectResult>(result);
            Assert.IsType<string>(httpResult.Value as string);
        }
    }
}
