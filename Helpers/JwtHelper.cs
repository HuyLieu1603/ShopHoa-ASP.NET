using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Web.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ShopHoa.Helpers
{
    public class JwtHelper
    {
        public static string GenerateToken(string email,int expireMinutes = 60)
        {
            var secretKey = WebConfigurationManager.AppSettings["JwtSecret"];
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "http://localhost:3000",
                audience: "http://localhost:3000",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expireMinutes),
                signingCredentials: credentials

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}