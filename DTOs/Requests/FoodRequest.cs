using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição para registrar e atualizar um alimento.
    /// </summary>
    public class FoodRequest
    {
        /// <summary>
        /// Nome do alimento.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Quantidade de calorias por porção do alimento.
        /// </summary>
        public decimal Calories { get; set; }

        /// <summary>
        /// Quantidade de proteína (em gramas) por porção do alimento.
        /// </summary>
        public decimal Proteins { get; set; }

        /// <summary>
        /// Quantidade de carboidratos (em gramas) por porção do alimento.
        /// </summary>
        public decimal Carbs { get; set; }

        /// <summary>
        /// Quantidade de gorduras (em gramas) por porção do alimento.
        /// </summary>
        public decimal Fats { get; set; }
    }
}
