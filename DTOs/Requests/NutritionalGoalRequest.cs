using MyFood.Models.Enums;

namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição para registrar e atualizar uma meta nutricional.
    /// </summary>
    public class NutritionalGoalRequest
    {
        /// <summary>
        /// Meta de calorias diárias (kcal).
        /// </summary>
        public int DailyCalories { get; set; }

        /// <summary>
        /// Percentual de calorias vindas da proteína.
        /// </summary>
        public int ProteinsPercentage { get; set; }

        /// <summary>
        /// Percentual de calorias vindas dos carboidratos.
        /// </summary>
        public int CarbsPercentage { get; set; }

        /// <summary>
        /// Percentual de calorias vindas das gorduras.
        /// </summary>
        public int FatsPercentage { get; set; }

        /// <summary>
        /// O objetivo de peso do usuário, como manter, ganhar ou perder peso.
        /// </summary>
        public GoalEnum WeightGoal { get; set; }
    }
}
