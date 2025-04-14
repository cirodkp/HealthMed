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
    public class AuthenticationDoctorServices(IConfiguration _configuration, IDoctorPublisher doctorPublisher) : IAuthenticationDoctorService
    {
        public async Task<DoctorCredentialsResponse> Execute(DoctorAuthenticationCommand doctorCredential)
        {
            if ((string.IsNullOrWhiteSpace(doctorCredential.Crm)) || (string.IsNullOrWhiteSpace(doctorCredential.Password)))
                throw new ArgumentException("CRM/Senha nulo ou vazio.");

            string passwordHash = PasswordService.CalculaPasswordHash_Sha512(doctorCredential.Password, doctorCredential.Crm);

            var loginEvent = new DoctorLoginEvent
            {
                Crm = doctorCredential.Crm,
                PasswordHash = passwordHash
            };

            // Solicita autenticação via RabbitMQ
            var response = await doctorPublisher.RequestLoginDoctorSync(loginEvent);

            if (!response.IsAuthenticated)
                throw new ArgumentException(response.ErrorMessage ?? "Credenciais inválidas.");

            var tokenHandler = new JwtSecurityTokenHandler();

            var chaveCriptografia = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("SecretJWT")!);

            var tokenPropriedades = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, doctorCredential.Crm),
                    new Claim(ClaimTypes.Name, response.Name ?? doctorCredential.Crm),
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
