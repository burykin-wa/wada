using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.TokenProviders
{
    internal class AuthOptions
    {
        public const string ISSUER = "WaveAccess";
        public const string AUDIENCE = "User";
        const string KEY = "5a34mo1r3k7KL75kFVGXnAYAB3MTayyg";   // encription key
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
