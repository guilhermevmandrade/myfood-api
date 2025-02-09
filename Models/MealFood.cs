using MyFood.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    /// <summary>
    /// Representa os alimentos de uma refeição, suas quantidades e unidades de medida.
    /// </summary>
    [Table("meal_food")]
    public class MealFood
    {
        /// <summary>
        /// Identificador único da relação entre alimento e refeição no banco de dados.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identificador da refeição.
        /// </summary>
        [Column("meal_id")]
        public int MealId { get; set; }

        /// <summary>
        /// Identificador do alimento.
        /// </summary>
        [Column("food_id")]
        public int FoodId { get; set; }

        /// <summary>
        /// Quantidade do alimento na refeição.
        /// </summary>
        [Column("quantity")]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Unidade de medida da quantidade de alimento na refeição.
        /// </summary>
        [Column("unit")]
        public MeasurementUnitEnum Unit { get; set; }
    }
}
