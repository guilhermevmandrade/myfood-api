using MyFood.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    [Table("meal_food")]
    public class MealFood
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("meal_id")]
        public int MealId { get; set; }

        [Column("food_id")]
        public int FoodId { get; set; }

        [Column("quantity")]
        public decimal Quantity { get; set; }

        [Column("unit")]
        public MeasurementUnit Unit { get; set; }
    }
}
