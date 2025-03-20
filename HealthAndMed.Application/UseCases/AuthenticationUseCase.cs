using HealthAndMed.Application.Commands;
using HealthAndMed.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthAndMed.Application.UseCases
{
    public class AuthenticationUseCase(IConfiguration _configuration) : IAuthenticationUseCase
    {
        public string GetToken(DoctorAuthenticationCommand command)
        {
            // Validar se o usuário Existe
            if ((string.IsNullOrWhiteSpace(command.Crm)) || (string.IsNullOrWhiteSpace(command.Password)))
            {
                return string.Empty;
            }

            if ((command.Crm != "admin") || (command.Password != "admin@123"))
            {
                return string.Empty;
            }

            // Gerar o token p/ o Usuario
            var tokenHandler = new JwtSecurityTokenHandler();

            // Recuperar a chave no appSettings
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

            return tokenHandler.WriteToken(token);
        }
    }
}
