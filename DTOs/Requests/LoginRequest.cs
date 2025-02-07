namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a resposta do endpoint "/login"/>.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// O email do usuário.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// A senha do usuário.
        /// </summary>
        public required string Password { get; init; }
    }
}
