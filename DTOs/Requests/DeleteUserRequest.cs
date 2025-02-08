namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição do endpoint DELETE "/user/me".
    /// </summary>
    public class DeleteUserRequest
    {
        /// <summary>
        /// A senha do usuário.
        /// </summary>
        public required string Password { get; init; }
    }
}
