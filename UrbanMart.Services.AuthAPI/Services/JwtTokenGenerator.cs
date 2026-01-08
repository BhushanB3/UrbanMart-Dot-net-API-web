using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UrbanMart.Services.AuthAPI.Models;
using UrbanMart.Services.AuthAPI.Models.DTO;
using UrbanMart.Services.AuthAPI.Services.IServices;

namespace UrbanMart.Services.AuthAPI.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string TokenGenerator(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // create read validate manage JSON tokens
            
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Firstname),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Expires = DateTime.UtcNow.AddMinutes(3)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
