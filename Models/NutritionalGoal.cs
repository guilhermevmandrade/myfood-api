using MyFood.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    /// <summary>
    /// Representa a meta nutricional de calorias e macronutrientes do usuário.
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
        /// Meta de calorias diárias (kcal).
        /// </summary>
        [Column("daily_calories")]
        public int DailyCalories { get; set; }

        /// <summary>
        /// Percentual de calorias vindas da proteína.
        /// </summary>
        [Column("proteins_percentage")]
        public int ProteinsPercentage { get; set; }

        /// <summary>
        /// Percentual de calorias vindas dos carboidratos.
        /// </summary>
        [Column("carbs_percentage")]
        public int CarbsPercentage { get; set; }

        /// <summary>
        /// Percentual de calorias vindas das gorduras.
        /// </summary>
        [Column("fats_percentage")]
        public int FatsPercentage { get; set; }

        /// <summary>
        /// O objetivo de peso do usuário, como manter, ganhar ou perder peso.
        /// </summary>
        [Column("weight_goal")]
        public GoalEnum WeightGoal { get; set; }

        /// <summary>
        /// Contrutor da entidade NutritionalGoal para o cadastro de novas metas nutricionais.
        /// </summary>
        /// <param name="userId">Identificador do usuário associado a essa meta pertence.</param>
        /// <param name="dailyCalories">Meta de calorias diárias (kcal).</param>
        /// <param name="proteinsPercentage">Percentual de calorias vindas da proteína.</param>
        /// <param name="carbsPercentage">Percentual de calorias vindas dos carboidratos.</param>
        /// <param name="fatsPercentage">Percentual de calorias vindas das gorduras.</param>
        /// <param name="weightGoal">O objetivo de peso do usuário.</param>
        public NutritionalGoal(int userId, int dailyCalories, int proteinsPercentage, int carbsPercentage, int fatsPercentage, GoalEnum weightGoal)
        {
            UserId = userId;
            DailyCalories = dailyCalories;
            ProteinsPercentage = proteinsPercentage;
            CarbsPercentage = carbsPercentage;
            FatsPercentage = fatsPercentage;
            WeightGoal = weightGoal;
        }
    }
}
