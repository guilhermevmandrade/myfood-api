using MyFood.Models.Enums;

namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição para atualizar a meta de calorias diárias.
    /// </summary>
    public class DailyCaloriesRequest
    {
        /// <summary>
        /// Meta de calorias diárias (kcal).
        /// </summary>
        public int DailyCalories { get; set; }

        /// <summary>
        /// O objetivo de peso do usuário, como manter, ganhar ou perder peso.
        /// </summary>
        public GoalEnum WeightGoal { get; set; }
    }
}
