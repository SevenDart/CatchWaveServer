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
        public const string Issuer = "CatchWaveAuthServer"; 
        public const string Audience = "CatchWaveAuthClient";
        const string Key = "thisKeyisnotsupersecret";
        private const int HoursOfTokenLifetime = 1;
        
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public static string CreateToken(int id, string username)
        {
            var claims = new List<Claim>
            {
                new Claim("Username", username),
                new Claim("Id", id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", "Username", ClaimTypes.Role);

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthTools.Issuer,
                audience: AuthTools.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: now.Add(TimeSpan.FromHours(AuthTools.HoursOfTokenLifetime)),
                signingCredentials: new SigningCredentials(AuthTools.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}