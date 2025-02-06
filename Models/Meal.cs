namespace MyFood.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public required string Description { get; set; }
        public DateTime MealTime { get; set; }
        public List<MealFood> MealFood { get; set; } = [];
    }
}
