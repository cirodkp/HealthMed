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
                    new Claim(ClaimTypes.Name, command.User)
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
    public class AuthenticationDoctorServices(IConfiguration _configuration) : IAuthenticationDoctorService
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
                    new Claim(ClaimTypes.Name, command.Cpf)
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
