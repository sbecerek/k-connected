using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using k_connected.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;

namespace k_connected
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {

        private readonly IDictionary<string, string> users = new Dictionary<string, string> { { "admin", "iamadmin" } };

        private readonly string key;

        private readonly kconnectedDBContext ctx;

        public JwtAuthenticationManager(string key)
        {
            this.key = key;
            ctx = new kconnectedDBContext();
        }
        
        public string Authenticate(string username, string password)
        {
            if (!users.Any(u => u.Key == username && u.Value == password) && !ctx.Entities.Any(u => u.Username == username && u.Passwd == password))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name , username)

                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                         SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }

    }
}
