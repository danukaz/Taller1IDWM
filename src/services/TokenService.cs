using System;
using System.Collections.Generic;
using System.IdentityModel.Token.JWT;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Taller.Src.Interfaces;
using Taller.Src.Models;

using Microsoft.IdentityModel.Tokens;

namespace Taller.Src.Service
{
    public class TokenService : ITokenServices
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;
            var signinkey = _config["JWT:SignInKey"] ?? throw new ArgumentException("Key not found");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinkey));
        } 
        public string GenerateToken(User user, string role)
        {
            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier, user.Id),
               new(JwtRegisteredClaimNames.Email, user.Email!),
               new(JwtRegisteredClaimNames.GivenName, user.FirstName),
               new(ClaimTypes.Role, role),
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}