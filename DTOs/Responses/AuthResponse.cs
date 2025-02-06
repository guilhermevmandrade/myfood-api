namespace MyFood.DTOs.Responses
{
    /// <summary>
    /// Representa a resposta de autenticação contendo o token JWT e informações do usuário autenticado.
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// Token JWT gerado para o usuário autenticado.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// E-mail do usuário autenticado.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Inicializa uma nova instância de <see cref="AuthResponse"/>.
        /// </summary>
        /// <param name="token">Token JWT gerado.</param>
        /// <param name="email">E-mail do usuário autenticado.</param>
        public AuthResponse(string token, string email)
        {
            Token = token;
            Email = email;
        }
    }
}
