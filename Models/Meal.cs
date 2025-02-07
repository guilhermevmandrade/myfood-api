using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    [Table("meal")]
    public class Meal
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("description")]
        public required string Description { get; set; }

        [Column("meal_time")]
        public DateTime MealTime { get; set; }
    }
}
