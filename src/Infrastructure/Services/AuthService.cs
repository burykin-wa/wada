using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Exceptions;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.Services;
using Infrastructure.Models.Auth;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenProvider _accessTokenProvider;

        public AuthService(IUserRepository userRepository, IAccessTokenProvider accessTokenProvider)
        {
            _userRepository = userRepository;
            _accessTokenProvider = accessTokenProvider;
        }

        public async Task<TokenSet> LoginAsync(string userName, string password)
        {
            var user = await _userRepository.GetUserByNameAsync(userName) ?? throw new ServiceException("User not found", StatusCodes.Status401Unauthorized);

            var encriptedPassword = PasswordEncryption.EncryptPassword(password);
            var isPasswordValid = user.Password == encriptedPassword;
            if (!isPasswordValid)
                throw new ServiceException("Invalid credentials", StatusCodes.Status401Unauthorized);

            var _refreshToken = await _accessTokenProvider.GenerateRefreshTokenAsync(user);
            var _accessToken = await _accessTokenProvider.GenerateAccessTokenAsync(user);

            return new TokenSet(_accessToken, _refreshToken);
        }

        public async Task<TokenSet> RefreshTokensAsync(string refreshToken)
        {
            var userName = _accessTokenProvider.GetUserNameFromToken(refreshToken) ?? throw new ServiceException("Invalid token");
            var user = await _userRepository.GetUserByNameAsync(userName) ?? throw new ServiceException("User not found");

            var _refreshToken = await _accessTokenProvider.GenerateRefreshTokenAsync(user);
            var _accessToken = await _accessTokenProvider.GenerateAccessTokenAsync(user);

            return new TokenSet(_accessToken, _refreshToken);
        }

        public async Task RegisterAsync(string userName, string password)
        {
            var user = await _userRepository.GetUserByNameAsync(userName);

            if (user is not null)
            {
                throw new ServiceException("User already exists", StatusCodes.Status409Conflict);
            }
            var encriptedPassword = PasswordEncryption.EncryptPassword(password);
            await _userRepository.InsertUserAsync(new UserEntity { Name = userName, Password = encriptedPassword });
        }
    }
}
