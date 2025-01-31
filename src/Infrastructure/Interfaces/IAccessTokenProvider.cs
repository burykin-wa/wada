using Core.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    internal interface IAccessTokenProvider
    {
        Task<string> GenerateAccessTokenAsync(UserEntity user);        
        Task<string> GenerateRefreshTokenAsync(UserEntity user);

        string? GetUserNameFromToken(string token);
    }
}
