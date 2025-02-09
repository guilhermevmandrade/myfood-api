using MyFood.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição do endpoint PUT "/user/me".
    /// </summary>
    public class UpdateUserRequest
    {
        /// <summary>
        /// O nome do usuário.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Altura do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Gênero do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Idade do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Peso do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Nível de Atividade para ajustar o gasto calórico diário com base no estilo de vida.
        /// </summary>
        public ActivityLevelEnum ActivityLevel { get; set; }
    }
}
