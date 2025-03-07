using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using System.Web.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShopHoa.Models;
using ShopHoa.Areas.Admin.Controllers.ManageAccount;
using System.Text.Json.Serialization;
using Newtonsoft.Json;



namespace ShopHoa.Helpers
{
    public class JwtHelper
    {
        //Generate Token
        public static string GenerateToken(string email,int expireMinutes = 60)
        {
            //GET: secret key
            var secretKey = WebConfigurationManager.AppSettings["JwtSecret"];
            //Secure secret key
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            AccountController account = new AccountController();
            var user = account.GetUserByEmail(email);

            //convert object to json
            var userDataJson = JsonConvert.SerializeObject(user, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            var claims = new[]
            {
                new Claim("userData",Base64UrlEncoder.Encode(userDataJson)),
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expireMinutes),
                signingCredentials: credentials

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //get token from cookie
        public static string GetTokenFromCookie()
        {
            HttpCookie authCookie = HttpContext.Current.Request.Cookies["AuthToken"];
            return authCookie?.Value;
        }


        // decode token to get data
        public static Staff ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WebConfigurationManager.AppSettings["JwtSecret"]));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
            };

            // decode token
            var principal = tokenHandler.ValidateToken(token, validationParameters,out var validatedToken);
            var userDataClaim = principal.Claims.FirstOrDefault(c=>c.Type == "userData")?.Value;
            string jsonData = Encoding.UTF8.GetString(Base64UrlEncoder.DecodeBytes(userDataClaim));

            if (jsonData != null)
            {
                return JsonConvert.DeserializeObject<Staff>(jsonData);
            }
            return null;
        }


        //get email from token
        public static string GetEmailFromToken()
        {
            var token = HttpContext.Current.Request.Cookies["AuthToken"]?.Value;
            if (string.IsNullOrEmpty(token)) return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var emailClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);
            return emailClaim?.Value;
        }
    }
}