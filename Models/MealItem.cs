using MyFood.Models.Enums;

namespace MyFood.Models
{
    public class MealItem
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public required Meal Meal { get; set; }
        public int FoodId { get; set; }
        public required Food Food { get; set; }
        public decimal Quantity { get; set; }
        public MeasurementUnit Unit { get; set; }
    }
}
