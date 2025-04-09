using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Application.Commands;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HealthMed.Application.Services
{
    public class AuthenticationAdminServices(IConfiguration _configuration) : IAuthenticationAdminService
    {
        public async Task<AdminCredentialsResponse> Execute(AdminAuthenticationCommand command)
        {
            if ((string.IsNullOrWhiteSpace(command.User)) || (string.IsNullOrWhiteSpace(command.Password)))
                throw new ArgumentException("Usuário/Senha nulo ou vazio.");

            // TODO: Buscar credenciais no banco de dados
            if ((command.User != "admin") || (command.Password != "admin@123"))
                throw new ArgumentException("Credenciais inválidas.");

            var tokenHandler = new JwtSecurityTokenHandler();

            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT")!);

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, command.User),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPropriedades);

            return new AdminCredentialsResponse(tokenHandler.WriteToken(token));
        }
    }
}
