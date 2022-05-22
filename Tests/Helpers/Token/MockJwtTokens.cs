using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Tests.Helpers.Token
{
    public static class MockJwtTokens
    {
        public static string Issuer { get; } = "www.hesapizi.com";
        public static string Audience { get; } = "www.hesapizi.com";
        public static SecurityKey SecurityKey { get; }
        public static SigningCredentials SigningCredentials { get; }

        private static readonly JwtSecurityTokenHandler STokenHandler = new();
        private static string _keyString = "!z2c3ggx3C4v5B*_*!z15!!2x3C4v5B*_*";

        static MockJwtTokens()
        {
            var sKey = Encoding.UTF8.GetBytes(_keyString);
            SecurityKey = new SymmetricSecurityKey(sKey);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);
        }

        public static string GenerateJwtToken(IEnumerable<Claim> claims, double value = 10)
        {
            return STokenHandler.WriteToken(new JwtSecurityToken(Issuer, Audience, claims, DateTime.Now, DateTime.Now.AddSeconds(value), SigningCredentials));
        }
    }
}
