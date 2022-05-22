using Core.Extensions;
using Core.Utilities.Security.Encyption;
using Entities.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public string DecodeToken(string input)
        {
            var handler = new JwtSecurityTokenHandler();
            if (input.StartsWith("Bearer "))
                input = input["Bearer ".Length..];
            return handler.ReadJwtToken(input).ToString();
        }

        public TAccessToken CreateToken<TAccessToken>(User user)
            where TAccessToken : IAccessToken, new()
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new TAccessToken()
            {
                Token = token,
                Expiration = _accessTokenExpiration,
                RefreshToken = user.RefreshToken
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
                SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(

                    issuer: tokenOptions.Issuer,
                    audience: tokenOptions.Audience,
                    expires: _accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims: SetClaims(user),
                    signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<System.Security.Claims.Claim> SetClaims(User user)
        {
            var claims = new List<System.Security.Claims.Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email ?? "");
            claims.AddIsAdmin(user.IsAdmin ? "1" : "0");
            if (!string.IsNullOrEmpty(user.FullName)) claims.AddName($"{user.FullName}");
            claims.AddCompanyId(user.CompanyId.ToString());
            return claims;
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal claims = null;
            try
            {
                var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
                claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidAudience = _tokenOptions.Audience,
                    IssuerSigningKey = securityKey
                }, out SecurityToken jwtToken);
                if ((jwtToken == null) || (jwtToken.ValidFrom > DateTime.UtcNow) || (jwtToken.ValidTo < DateTime.UtcNow))
                    return false;
            }
            catch (Exception)
            {
            }
            return claims != null;
        }
    }
}
