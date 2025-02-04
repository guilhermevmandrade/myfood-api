namespace MyFood.Models
{
    public class Food
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbs { get; set; }
        public decimal Fats { get; set; }
    }
}
