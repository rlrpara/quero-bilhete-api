using EmbarqueComAGente.Domain.Entities;
using EmbarqueComAGente.Infra.CrossCutting.Auth.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace EmbarqueComAGente.Infra.CrossCutting.Auth.Services
{
    public static class TokenService
    {
        public static string GenerateToken(Usuario usuario)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Settings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Email, usuario.Email),
                        new Claim(ClaimTypes.NameIdentifier, usuario.Codigo.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch
            {
                return default(dynamic);
            }
        }

        public static string GetValueFromClaim(IIdentity identity, string field)
        {
            var claims = identity as ClaimsIdentity;

            return claims.FindFirst(field).Value;
        }
    }
}
