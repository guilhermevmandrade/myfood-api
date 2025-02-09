using MyFood.Models.Enums;

namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição para vincular um alimento à refeição.
    /// </summary>
    public class MealFoodRequest
    {
        /// <summary>
        /// Quantidade do alimento na refeição.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Unidade de medida da quantidade de alimento na refeição.
        /// </summary>
        public MeasurementUnit Unit { get; set; }
    }
}
