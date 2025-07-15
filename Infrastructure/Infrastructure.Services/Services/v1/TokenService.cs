using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CrossCutting.Configuration;
using Infrastructure.Services.Interfaces.v1;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Services.v1
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings _appSettings;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public string GerarToken(string email, string perfil)
        {
            try
            {
                var configuracoesJwt = _appSettings.Jwt;

                var claims = new[]
                {
                    new Claim("email", email),
                    new Claim(ClaimTypes.Role, perfil)
                };

                var chaveDeSeguranca = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuracoesJwt.Chave));
                var credenciais = new SigningCredentials(chaveDeSeguranca, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: configuracoesJwt.Emissor,
                    audience: configuracoesJwt.Audiencia,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(configuracoesJwt.ExpiracaoEmMinutos),
                    signingCredentials: credenciais
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao gerar o token jwt: {ex.Message}");
            }
        }
    }
}
