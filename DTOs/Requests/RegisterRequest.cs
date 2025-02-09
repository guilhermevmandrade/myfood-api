using MyFood.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição do endpoint "/register".
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

        /// <summary>
        /// Gênero do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Altura do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Peso do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// Nível de Atividade para ajustar o gasto calórico diário com base no estilo de vida.
        /// </summary>
        public ActivityLevelEnum ActivityLevel { get; set; }
    }
}
