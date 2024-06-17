using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Whiteboard.Service.Middleware
{
    public class KeycloakJwtHandler(IConfiguration configuration) : IJwtHandler
    {
        private readonly IConfiguration _configuration = configuration;

        private TokenValidationParameters _parameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false
        };

        private readonly JsonWebTokenHandler tokenHandler = new();

        public async Task<bool> IsValidJWT(JsonWebToken token)
        {
            _parameters.IssuerSigningKeys ??= await FetchKeysAsync();

            var result = await tokenHandler.ValidateTokenAsync(token, _parameters);
            return result.IsValid;
        }

        public async Task<IEnumerable<Claim>> GetClaimsAsync(JsonWebToken token)
        {
            _parameters.IssuerSigningKeys ??= await FetchKeysAsync();

            var result = await tokenHandler.ValidateTokenAsync(token, _parameters);
            return result.ClaimsIdentity.Claims;
        }

        private async Task<IEnumerable<SecurityKey>> FetchKeysAsync()
        {
            var handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            var jwksUrl = _configuration["KC_JWKS_URL"]
                ?? throw new ArgumentNullException("KC_JWKS_URL");

            var response = await httpClient.GetAsync(jwksUrl);
            response.EnsureSuccessStatusCode();
            var jwksJson = await response.Content.ReadAsStringAsync();

            // Parse JWKS
            var jwks = JsonSerializer.Deserialize<Jwks>(jwksJson)
                ?? throw new JsonException($"Could not deserialize json: {jwksJson}");

            return jwks.keys.Select(k => new X509SecurityKey(new X509Certificate2(Convert.FromBase64String(k.x5c.First()))));
        }

        private class Jwks
        {
            public IEnumerable<Jwk> keys { get; set; } = [];

            public class Jwk
            {
                public List<string> x5c { get; set; } = [];
            }
        }
    }
}
