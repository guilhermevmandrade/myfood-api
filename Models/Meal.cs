using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    /// <summary>
    /// Representa uma refeição, sua descrição, seus alimentos e seu horário.
    /// </summary>
    [Table("meal")]
    public class Meal
    {
        /// <summary>
        /// Identificador único da refeição no banco de dados.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identificador do usuário proprietário desta refeição.
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Descrição textual da refeição.
        /// </summary>
        [Column("description")]
        public string Description { get; set; }

        /// <summary>
        /// Data e horário da refeição.
        /// </summary>
        [Column("meal_time")]
        public DateTime MealTime { get; set; }

        /// <summary>
        /// Contrutor da entidade Meal para o cadastro de novas refeições.
        /// </summary>
        /// <param name="userId">Identificador do usuário proprietário desta refeição.</param>
        /// <param name="description">Descrição textual da refeição.</param>
        /// <param name="mealTime">Data e horário da refeição.</param>
        public Meal(int userId, string description, DateTime mealTime)
        {
            UserId = userId;
            Description = description;
            MealTime = mealTime;
        }
    }
}
