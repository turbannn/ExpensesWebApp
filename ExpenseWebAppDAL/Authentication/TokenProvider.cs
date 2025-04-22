using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace ExpenseWebAppDAL.Authentication
{
    public class TokenProvider(IConfiguration configuration)
    {
        public string CreateToken(int id, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));

            var securityCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var descr = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Typ, role)
                ]),
                Expires = DateTime.Now.AddMinutes(15),
                Issuer = configuration["JwtSettings:Issuer"],
                Audience = configuration["JwtSettings:Audience"],
                SigningCredentials = securityCred
            };

            var handler = new JsonWebTokenHandler();

            return handler.CreateToken(descr);
        }
    }
}
