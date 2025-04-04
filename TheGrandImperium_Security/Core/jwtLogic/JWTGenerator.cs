using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheGrandImperium_Security.Core.Entities;

namespace TheGrandImperium_Security.Core.jwtLogic
{
    public class JWTGenerator : IJWTGenerator
    {
       private readonly IConfiguration _configuration;

        public JWTGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1), // ✅ Corregido a UTC
                NotBefore = DateTime.UtcNow, // Opcional: Define un inicio válido en UTC
                IssuedAt = DateTime.UtcNow, // Opcional: Marca el momento en que se generó el token
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
