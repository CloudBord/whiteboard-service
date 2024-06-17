using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Whiteboard.Service.Middleware
{
    public interface IJwtHandler
    {
        Task<bool> IsValidJWT(JsonWebToken token);
        Task<IEnumerable<Claim>> GetClaimsAsync(JsonWebToken token);
    }
}
