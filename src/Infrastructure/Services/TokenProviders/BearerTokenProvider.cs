using Core.Models;
using Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Services.TokenProviders
{
	internal class BearerTokenProvider: IAccessTokenProvider
    {
        public static int SECONDS_TO_TOKEN = 60;
        public static int SECONDS_TO_REFRESH_TOKEN = 86400;

        private readonly JwtSecurityTokenHandler _tokenHandler;
        public BearerTokenProvider()
        {
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public async Task<string> GenerateAccessTokenAsync(UserEntity user)
        {
#if DEBUG
            return await GenerateAsync(user, SECONDS_TO_TOKEN * 60);
#else
            return await GenerateAsync(user, SECONDS_TO_TOKEN);
#endif
		}
		public async Task<string> GenerateRefreshTokenAsync(UserEntity user)
        {
            return await GenerateAsync(user, SECONDS_TO_REFRESH_TOKEN);
        }

        private async Task<string> GenerateAsync(UserEntity user, int tokenExpirationSeconds)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),                
            };

            DateTime expires = DateTime.UtcNow.Add(TimeSpan.FromSeconds(tokenExpirationSeconds));

            var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
                    claims: claims,
                    expires: expires,
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = _tokenHandler.WriteToken(jwt);

            return await Task.FromResult(token);
        }

        public string? GetUserNameFromToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            var jwtSecurityToken = _tokenHandler.ReadJwtToken(token);
            return jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        }
        

        internal static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = AuthOptions.ISSUER,

                ValidateAudience = true,
                ValidAudience = AuthOptions.AUDIENCE,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),

                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
            };
        }
    }
}
