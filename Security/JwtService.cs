using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyFood.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyFood.Security
{
    /// <summary>
    /// Serviço responsável por gerar e validar tokens JWT (JSON Web Tokens).
    /// O JWT é usado para autenticação e autorização, permitindo que o cliente envie um token com cada requisição ao servidor para acessar recursos protegidos.
    /// </summary>
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;

        /// <summary>
        /// Construtor que recebe as configurações do JWT via injeção de dependência.
        /// </summary>
        /// <param name="jwtSettings">Configurações do token JWT, incluindo chave secreta, emissor e tempo de expiração.</param>
        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Gera um token JWT para um usuário autenticado.
        /// O token contém claims (informações sobre o usuário) e uma assinatura criptografada.
        /// </summary>
        /// <param name="user">Objeto User contendo informações do usuário autenticado.</param>
        /// <returns>Token JWT em formato string.</returns>
        public string GenerateToken(User user)
        {
            // Claims são informações do usuário dentro do token
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email)
            };

            // Gera a chave secreta usada para assinar o token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            // Credenciais de assinatura do token, a chave e o algoritmo hashing de assinatura (HMAC SHA256)
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Criando o token JWT com as informações do usuário
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: credentials
            );

            // Retorna o token como string para ser enviado ao cliente
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
