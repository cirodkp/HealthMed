using HealthMed.Auth.Application.Interfaces;
using HealthMed.Auth.Application.ViewModels;
using HealthMed.Auth.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HealthMed.Auth.Application.UseCases
{
    public class LoginUseCase(IConfiguration configuration, IUsuarioRepository repository) : ILoginUseCase
    {
        public async Task<LoginResponse?> ExecuteAsync(LoginRequest request)
        {
            // 1. Validação de entrada
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Senha))
                return null;

            // 2. Buscar usuário
            var usuario = await repository.ObterPorLoginAsync(request.Login);
            if (usuario == null)
                return null;

            // 3. Verificar senha
            var senhaValida = await repository.VerificarSenhaAsync(usuario, request.Senha);
            if (!senhaValida)
                return null;

            // 4. Validar secret JWT
           var secret = configuration["SecretJWT"]; //configuration.GetValue<string>("SecretJWT");
            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("SecretJWT não configurado.");

            var chaveCriptografia = Encoding.ASCII.GetBytes(secret);

            // 5. Gerar token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome),
                    new Claim(ClaimTypes.Role, usuario.Role)
                }),
                            Expires = DateTime.UtcNow.AddHours(4),
                            Issuer = "HealthMed.Auth.API",
                            Audience = "healthmed-api", // ou "medico-api" se for específico
                            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(chaveCriptografia),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 6. Retornar resposta
            return new LoginResponse
            {
                Token = tokenHandler.WriteToken(token),
                Role = usuario.Role,
                Nome = usuario.Nome
            };
        }
    }
}
