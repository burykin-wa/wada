using Infrastructure.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(string userName, string password);

        Task<TokenSet> LoginAsync(string userName, string password);

        Task<TokenSet> RefreshTokensAsync(string refreshToken);
    }
}
