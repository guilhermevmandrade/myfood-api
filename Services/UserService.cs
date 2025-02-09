using MyFood.Data;
using MyFood.Data.Repositories.Interfaces;
using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models;
using MyFood.Security;
using MyFood.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MyFood.Services
{
    /// <summary>
    /// Implementação do serviço de usuários, responsável por autenticação, registro, busca e exclusão.
    /// Esta classe usa um repositório de usuários e um serviço JWT para login e criação de contas.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtService _jwtService;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para autenticação e manipulação de usuários.
        /// </summary>
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, JwtService jwtService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registra um novo usuário no sistema, verificando se o e-mail já existe.
        /// Se o e-mail for único, cria um novo usuário e retorna o objeto criado.
        /// </summary>
        /// <param name="request">Dados cadastrais do usuário.</param>
        public async Task RegisterAsync(RegisterRequest request)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                if (await _userRepository.GetByEmailAsync(request.Email) != null)
                {
                    throw new Exception("Email já cadastrado.");
                }

                var user = new User(
                    request.Name,
                    request.Email,
                    request.Gender,
                    request.Age,
                    request.Height,
                    request.Weight,
                    request.ActivityLevel,
                    HashPassword(request.Password)
                );

                await _userRepository.CreateAsync(user);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }

        /// <summary>
        /// Autentica um usuário com base no e-mail e senha fornecidos.
        /// Se a autenticação for bem-sucedida, um token JWT é gerado.
        /// </summary>
        /// <param name="email">Endereço de e-mail do usuário.</param>
        /// <param name="password">Senha do usuário.</param>
        /// <returns>
        /// Um objeto <see cref="AuthResponse"/> contendo o token JWT e dados do usuário.
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
                throw new Exception("Senha incorreta.");
            }

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse(token, user.Name, user.Email);
        }

        /// <summary>
        /// Obtém os principais dados do usuário.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns>
        /// Um objeto <see cref="GetUserResponse"/> com os principais dados do usuário.
        /// </returns>
        public async Task<GetUserResponse> GetUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            return user;
        }

        /// <summary>
        /// Atualiza os dados do usuário.
        /// </summary>
        /// <param name="request">Novos dados do usuário a ser atualizado.</param>
        /// <param name="id">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task UpdateUserAsync(UpdateUserRequest request, int id)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool userExists = await _userRepository.UserExistsAsync(id);
                if (!userExists)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                await _userRepository.UpdateAsync(request, id);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Deleta o usuário com identificador correspondente.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <param name="email">Email do usuário para confirmação de exclusão.</param>
        /// <param name="password">Senha do usuário para confirmação de exclusão.</param>
        /// <returns></returns>
        public async Task DeleteUserAsync(int id, string email, string password)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var user = await _userRepository.GetByEmailAsync(email);
                if (user == null)
                {
                    throw new Exception("Usuário não encontrado.");
                }

                if (!VerifyPassword(password, user.PasswordHash))
                {
                    throw new Exception("Senha incorreta.");
                }

                await _userRepository.DeleteAsync(id);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
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
