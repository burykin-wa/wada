using Api.Models;
using Infrastructure.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
	[Route("api/v1/auth")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("signup")]
        [HttpPost]
		[SwaggerOperation(OperationId = "signup", Tags = ["Auth"])]
		public async Task<ActionResult> Register([FromBody] AuthRequestDto request)
        {
            await _authService.RegisterAsync(request.Username, request.Password);

            return Created();
        }

        [Route("token")]
        [HttpPost]
		[SwaggerOperation(OperationId = "authenticate", Tags = ["Auth"])]
		public async Task<ActionResult<TokenResponse>> Login([FromBody] AuthRequestDto request)
        {
            var tokens = await _authService.LoginAsync(request.Username, request.Password);

            return new TokenResponse()
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }

        [Route("token/refresh")]
        [HttpPost]
		[SwaggerOperation(OperationId = "refreshToken", Tags = ["Auth"])]
		public async Task<ActionResult<TokenResponse>> RefreshAccessToken([FromHeader(Name = "Refresh")] string refreshToken)
        {
            var tokens = await _authService.RefreshTokensAsync(refreshToken);

            return new TokenResponse()
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken
            };
        }

        [Authorize]
        [Route("current")]
        [HttpPost]
		[SwaggerOperation(OperationId = "getCurrentUserInfo", Tags = ["Auth"])]
		public async Task<ActionResult<UserDto>> GetCurrentUserInfo()
        {
            return await Task.FromResult(new UserDto { UserName = User.Identity!.Name! });
        }
    }
}
