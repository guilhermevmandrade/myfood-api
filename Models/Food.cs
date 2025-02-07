using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    [Table("food")]
    public class Food
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public required string Name { get; set; }

        [Column("calories")]
        public decimal Calories { get; set; }

        [Column("protein")]
        public decimal Protein { get; set; }

        [Column("carbs")]
        public decimal Carbs { get; set; }

        [Column("fats")]
        public decimal Fats { get; set; }
    }
}
