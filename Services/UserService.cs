using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MyFood.Data.Repositories;
using MyFood.Data.Repositories.Interfaces;
using MyFood.DTOs.Responses;
using MyFood.Models;
using MyFood.Security;
using MyFood.Services.Interfaces;

namespace MyFood.Services
{
    /// <summary>
    /// Implementação do serviço de usuários, responsável por autenticação e registro.
    /// Esta classe usa um repositório de usuários e um serviço JWT para login e criação de contas.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para autenticação e manipulação de usuários.
        /// </summary>
        public UserService(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Autentica um usuário com base no e-mail e senha fornecidos.
        /// Se a autenticação for bem-sucedida, um token JWT é gerado.
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>
        /// Um objeto <see cref="AuthResponse"/> contendo o token JWT e o ID do usuário.
        /// </returns>
        public async Task<AuthResponse> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }
            if (!VerifyPassword(password, user.PasswordHash))
            {
                throw new Exception("Dados incorretos.");
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse(
                token,
                user.Email
            );
        }

        /// <summary>
        /// Registra um novo usuário no sistema, verificando se o e-mail já existe.
        /// Se o e-mail for único, cria um novo usuário e retorna o objeto criado.
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">Endereço de e-mail do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>
        /// Um objeto <see cref="User"/> representando o usuário criado.
        /// </returns>
        public async Task RegisterAsync(string name, string email, string password)
        {
            if (await _userRepository.GetByEmailAsync(email) != null)
            {
                throw new Exception("Email já cadastrado.");
            }

            var user = new User(name, email, HashPassword(password));

            await _userRepository.CreateAsync(user);
        }

        /// <summary>
        /// Gera um hash seguro para a senha do usuário usando SHA-256.
        /// </summary>
        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        /// <summary>
        /// Verifica se a senha fornecida corresponde ao hash armazenado.
        /// </summary>
        private static bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }
}
