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
    public class AuthenticationPatientServices(IConfiguration _configuration) : IAuthenticationPatientService
    {
        public async Task<PatientCredentialsResponse> Execute(PatientAuthenticationCommand command)
        {
            if ((string.IsNullOrWhiteSpace(command.Cpf)) || (string.IsNullOrWhiteSpace(command.Password)))
                throw new ArgumentException("CPF/Senha nulo ou vazio.");

            // TODO: Buscar credenciais no banco de dados
            if ((command.Cpf != "11111111111") || (command.Password != "admin@123"))
                throw new ArgumentException("Credenciais inválidas.");

            var tokenHandler = new JwtSecurityTokenHandler();

            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT")!);

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, command.Cpf),
                    new Claim(ClaimTypes.Role, "Patient")
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPropriedades);

            return new PatientCredentialsResponse(tokenHandler.WriteToken(token));
        }
    }
}
