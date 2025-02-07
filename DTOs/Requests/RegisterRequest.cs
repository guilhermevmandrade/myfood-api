namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a resposta do endpoint "/register"/>.
    /// </summary>
    public sealed class RegisterRequest
    {
        /// <summary>
        /// O nome do usuário.
        /// </summary>
        public required string Name { get; init; }

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
