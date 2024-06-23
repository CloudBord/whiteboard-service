using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.Services
{
    public class ClaimsHandler : IClaimsHandler
    {
        public Guid GetUserId(FunctionContext executionContext)
        {
            executionContext.Items.TryGetValue("Claims", out object? output);
            IEnumerable<Claim>? claims = output as IEnumerable<Claim>;
            if (claims == null || !claims.Any())
            {
                throw new UnauthorizedAccessException("Claims cannot be null");
            }
            Claim? user = claims.Where(c => c.Type == "sub").FirstOrDefault() ?? throw new UnauthorizedAccessException("No user ID found in claims");
            return new Guid(user.Value.ToString());
        }
    }
}