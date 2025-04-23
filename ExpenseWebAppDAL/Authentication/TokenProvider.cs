using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace ExpenseWebAppDAL.Authentication
{
    public class TokenProvider
    {
        private readonly IConfiguration _configuration;
        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateAccessToken(int id, string name)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));

            var securityCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var descr = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, name)
                ]),
                Expires = DateTime.Now.AddMinutes(15),
                Issuer = _configuration["JwtSettings:Issuer"],
                Audience = _configuration["JwtSettings:Audience"],
                SigningCredentials = securityCred
            };

            var handler = new JsonWebTokenHandler();

            return handler.CreateToken(descr);
        }
    }
}
