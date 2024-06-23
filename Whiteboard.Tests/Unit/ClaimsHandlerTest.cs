using Microsoft.Azure.Functions.Worker;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Whiteboard.Service.Services;

namespace Whiteboard.Tests.Unit
{
    public class ClaimsHandlerTest
    {
        private readonly Mock<FunctionContext> _functionContextMock;
        private readonly ClaimsHandler _claimsHandler;

        public ClaimsHandlerTest()
        {
            _functionContextMock = new Mock<FunctionContext>();
            _claimsHandler = new ClaimsHandler();
        }

        [Fact]
        public void ClaimsHandlerTest_handleValidClaim()
        {
            // arrange
            Guid expectedUserId = Guid.NewGuid();

            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            new List<Claim>
                            {
                                new( "sub", expectedUserId.ToString())
                            }
                        }
                    }
                );

            // act
            Guid actualUserId = _claimsHandler.GetUserId(_functionContextMock.Object);

            // assert
            Assert.Equal(expectedUserId, actualUserId);
        }

        [Fact]
        public void ClaimsHandlerTest_handleInvalidClaims_emptyClaims()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>()
                );

            // act & assert
            Assert.Throws<UnauthorizedAccessException>(() =>
            {
                _claimsHandler.GetUserId(_functionContextMock.Object);
            });
        }

        [Fact]
        public void ClaimsHandlerTest_handleInvalidClaims_nullClaims()
        {
            // arrange
            _functionContextMock.Setup(x => x.Items)
                .Returns(
                    new Dictionary<object, object>
                    {
                        {
                            "Claims",
                            null
                        }
                    }
                );

            // act & assert
            Assert.Throws<UnauthorizedAccessException>(() =>
            {
                _claimsHandler.GetUserId(_functionContextMock.Object);
            });
        }

        [Fact]
        public void ClaimsHandlerTest_handleInvalidClaims_noSub()
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
                                new("invalid", "invalid")
                            }
                        }
                    }
                );

            // act & assert
            Assert.Throws<UnauthorizedAccessException>(() =>
            {
                _claimsHandler.GetUserId(_functionContextMock.Object);
            });
        }

        [Fact]
        public void ClaimsHandlerTest_handleInvalidClaims_emptySub()
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

            // act & assert
            Assert.Throws<FormatException>(() =>
            {
                _claimsHandler.GetUserId(_functionContextMock.Object);
            });
        }

        [Fact]
        public void ClaimsHandlerTest_handleInvalidClaims_invalidGuid()
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

            // act & assert
            Assert.Throws<FormatException>(() =>
            {
                _claimsHandler.GetUserId(_functionContextMock.Object);
            });
        }
    }
}
