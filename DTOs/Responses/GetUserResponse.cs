using MyFood.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.DTOs.Responses
{
    /// <summary>
    /// Representa a resposta do endpoint GET "/user/me".
    /// </summary>
    public class GetUserResponse
    {
        /// <summary>
        /// Nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Endereço de email do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Altura do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Peso do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Nível de Atividade para ajustar o gasto calórico diário com base no estilo de vida.
        /// </summary>
        public ActivityLevel ActivityLevel { get; set; }

        /// <summary>
        /// Construtor do GetUserResponse com os principais dados do usuário.
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">Endereço de email do usuário.</param>
        /// <param name="height">Altura do usuário para cálculos de Taxa Metabólica Basal.</param>
        /// <param name="weight">Peso do usuário para cálculos de Taxa Metabólica Basal.</param>
        /// <param name="activityLevel">Nível de Atividade para ajustar o gasto calórico diário com base no estilo de vida.</param>
        public GetUserResponse(string name, string email, double height, double weight, ActivityLevel activityLevel)
        {
            Name = name;
            Email = email;
            Height = height;
            Weight = weight;
            ActivityLevel = activityLevel;
        }
    }
}
