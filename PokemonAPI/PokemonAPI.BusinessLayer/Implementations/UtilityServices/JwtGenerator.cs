using Microsoft.IdentityModel.Tokens;
using PokemonAPI.BusinessLayer.Interfaces;
using PokemonAPI.BusinessLayer.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonAPI.BusinessLayer.Implementations.UtilityServices
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JwtSettings _settings;

        public JwtGenerator(JwtSettings settings)
        {
            _settings = settings;
        }

        public string GenerateToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtSecretKey = Encoding.ASCII.GetBytes(_settings.JwtSecret);

            var claims = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = null,
                Issuer = null,
                Subject = claims,
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwtSecretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = tokenHandler.WriteToken(token);

            return serializedToken;
        }
    }
}