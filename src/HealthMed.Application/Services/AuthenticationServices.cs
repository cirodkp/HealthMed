using HealthMed.Application.Commands;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using HealthMed.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthMed.Application.Services
{
    public class AuthenticationServices(
        IConfiguration _configuration) : IAuthenticationService
    {
        public async Task<DoctorCredentialsResponse> Execute(DoctorAuthenticationCommand command)
        {
            if ((string.IsNullOrWhiteSpace(command.Crm)) || (string.IsNullOrWhiteSpace(command.Password)))
                throw new ArgumentException("CRM/Senha nulo ou vazio.");

            // TODO: Buscar credenciais no banco de dados
            if ((command.Crm != "admin") || (command.Password != "admin@123"))
                throw new ArgumentException("Credenciais inválidas.");

            var tokenHandler = new JwtSecurityTokenHandler();

            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT")!);

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, command.Crm)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenPropriedades);

            return new DoctorCredentialsResponse(tokenHandler.WriteToken(token));
        }
    }
}
