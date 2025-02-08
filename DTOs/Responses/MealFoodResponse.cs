using MyFood.Models.Enums;

namespace MyFood.DTOs.Responses
{
    /// <summary>
    /// Representa a requisição para listar os dados dos alimentos em uma refeição.
    /// </summary>
    public class MealFoodResponse
    {
        /// <summary>
        /// Identificador único da relação entre refeição e alimento.
        /// </summary>
        public int MealFoodId { get; set; }

        /// <summary>
        /// Quantidade do alimento na refeição.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Unidade de medida da quantidade (ex: gramas, unidades, etc).
        /// </summary>
        public MeasurementUnit Unit { get; set; }

        /// <summary>
        /// Alimento que compõe a refeição.
        /// </summary>
        public required FoodResponse Food { get; set; }

        /// <summary>
        /// Contrutor que representa a resposta de uma relação de alimentos e refeições no sistemas
        /// </summary>
        /// <param name="mealfoodid">Identificador da relação entre refeição e alimento.</param>
        /// <param name="quantity">Quantidade do alimento na refeição.</param>
        /// <param name="unit">Unidade de medida da quantidade (ex: gramas, unidades, etc).</param>
        public MealFoodResponse(int mealfoodid, decimal quantity, MeasurementUnit unit)
        {
            MealFoodId = mealfoodid;
            Quantity = quantity;
            Unit = unit;
        }
    }
}
