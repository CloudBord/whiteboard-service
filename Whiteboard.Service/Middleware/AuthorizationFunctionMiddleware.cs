using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Net;

namespace Whiteboard.Service.Middleware
{
    public class AuthorizationFunctionMiddleware(IJwtHandler jwtHandler) : IFunctionsWorkerMiddleware
    {
        private readonly IJwtHandler _jwtHandler = jwtHandler;

        public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            var request = await context.GetHttpRequestDataAsync();
            if (request == null)
            {
                throw new Exception();
            }
            if (request.Headers.TryGetValues("Authorization", out var authorization))
            {
                JsonWebToken token;
                try
                {
                    var step1 = authorization.ToArray();
                    var step2 = authorization.ToArray()[0];
                    token = new JsonWebToken(authorization.ToArray()[0]![7..]);
                }
                catch
                {
                    var response = request.CreateResponse(HttpStatusCode.BadRequest);
                    await response.WriteStringAsync("Malformed token");
                    context.GetInvocationResult().Value = response;
                    return;
                }

                if (!await _jwtHandler.IsValidJWT(token))
                {
                    var response = request.CreateResponse(HttpStatusCode.Unauthorized);
                    await response.WriteStringAsync("Invalid token");
                    context.GetInvocationResult().Value = response;
                    return;
                }

                var claims = await _jwtHandler.GetClaimsAsync(token);
                context.Items["Claims"] = claims;
            }

            await next(context);
        }
    }
}
