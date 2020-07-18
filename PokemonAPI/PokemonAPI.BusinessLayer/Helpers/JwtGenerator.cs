using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PokemonAPI.BusinessLayer.Helpers
{
    public class JwtGenerator
    {
        public static string GenerateToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // TODO: put key into settings
            var jwtSecretKey = Encoding.ASCII.GetBytes("MyKeyIsExtraLargeAndSecretAndNeedsMoreCharactersToAddHereBecauseOtherwiseYouWouldGetException");

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