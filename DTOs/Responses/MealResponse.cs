using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.DTOs.Responses
{
    /// <summary>
    /// Representa a requisição para listar os dados de uma refeição.
    /// </summary>
    public class MealResponse
    {
        /// <summary>
        /// Identificador único da refeição no banco de dados.
        /// </summary>
        public int MealId { get; set; }

        /// <summary>
        /// Descrição textual da refeição.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Data e horário da refeição.
        /// </summary>
        public DateTime MealTime { get; set; }

        /// <summary>
        /// Alimentos que compõem a refeição.
        /// </summary>
        public required IList<MealFoodResponse> MealFoods { get; set; }

        /// <summary>
        /// Construtor que representa a resposta de uma refeição cadastrada no sistema.
        /// </summary>
        /// <param name="mealid">Identificador da refeição no banco de dados.</param>
        /// <param name="description">Descrição textual da refeição.</param>
        /// <param name="mealTime">Data e horário da refeição.</param>
        public MealResponse(int mealid, string description, DateTime mealTime)
        {
            MealId = mealid;
            Description = description;
            MealTime = mealTime;
        }
    }
}
