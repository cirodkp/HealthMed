using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HealthMed.Application.Commands;
using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HealthMed.Application.Services
{
    public class AuthenticationDoctorServices(IConfiguration _configuration,
    IDoctorPublisher doctorPublisher) : IAuthenticationDoctorService
    {
        public async Task<DoctorCredentialsResponse> Execute(DoctorAuthenticationCommand command)
        {
            if ((string.IsNullOrWhiteSpace(command.Crm)) || (string.IsNullOrWhiteSpace(command.Password)))
                throw new ArgumentException("CRM/Senha nulo ou vazio.");

            string passwordHash = PasswordService.CalculaPasswordHash_Sha512(command.Password, command.Crm);

            var loginEvent = new DoctorLoginEvent
            {
                Crm = command.Crm,
                PasswordHash = passwordHash
            };

            // Solicita autenticação via RabbitMQ
            var isAuthenticated = await doctorPublisher.RequestLoginDoctorSync(loginEvent);

            if (!isAuthenticated)
                throw new ArgumentException("Credenciais inválidas.");

            var tokenHandler = new JwtSecurityTokenHandler();

            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT")!);

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, command.Crm),
                    new Claim(ClaimTypes.Role, "Doctor")
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
