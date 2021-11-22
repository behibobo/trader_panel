using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TraderPanel.Core.Entities;

namespace TraderPanel.Auth.Services
{
    public static class TokenHandler
    {
        public static IConfiguration _configuration;
        public static dynamic CreateAccessToken(User user)
        {

            var audienceConfig = _configuration.GetSection("Audience");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Iss"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
            
            DateTime now = DateTime.UtcNow;

            JwtSecurityToken jwt = new JwtSecurityToken(
                     issuer: audienceConfig["Iss"],
                     audience: audienceConfig["Aud"],
                     claims: new List<Claim> {
                         new Claim("UserId", user.Id.ToString())
                     },
                     notBefore: now,
                     expires: now.Add(TimeSpan.FromMinutes(2)),
                     signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                 );
            return new
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                Expires = TimeSpan.FromHours(2).TotalSeconds
            };
        }
    }
}
