using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CatchWave
{
    public static class AuthTools
    {
        public const string ISSUER = "CatchWaveAuthServer"; 
        public const string AUDIENCE = "CatchWaveAuthClient";
        const string KEY = "thisKeyisnotsupersecret";
        public const int LIFETIME = 1;
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public static string CreateToken(int id, string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Sid, id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimTypes.Email, ClaimTypes.Role);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthTools.ISSUER,
                audience: AuthTools.AUDIENCE,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromHours(AuthTools.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthTools.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}