using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    /// <summary>
    /// Meta nutricional
    /// </summary>
    [Table("nutritional_goal")]
    public class NutritionalGoal
    {
        /// <summary>
        /// Identificador único da meta nutricional no banco de dados.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identificador do usuário associado a essa meta pertence.
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Meta de calorias diárias.
        /// </summary>
        [Column("daily_calories")]
        public int DailyCalories { get; set; }

        /// <summary>
        /// Percentual de calorias vindas da proteína.
        /// </summary>
        [Column("protein_percentage")]
        public double ProteinPercentage { get; set; }

        /// <summary>
        /// Percentual de calorias vindas dos carboidratos.
        /// </summary>
        [Column("carbs_percentage")]
        public double CarbsPercentage { get; set; }

        /// <summary>
        /// Percentual de calorias vindas das gorduras
        /// </summary>
        [Column("fat_percentage")]
        public double FatPercentage { get; set; }
    }
}
