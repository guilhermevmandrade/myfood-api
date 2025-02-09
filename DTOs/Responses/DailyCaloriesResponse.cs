using MyFood.Models.Enums;

namespace MyFood.DTOs.Responses
{
    /// <summary>
    /// Representa a resposta da requisição para obter a meta de calorias diárias consumida pelo usuário.
    /// </summary>
    public class DailyCaloriesResponse
    {
        /// <summary>
        /// Meta de calorias diárias (kcal).
        /// </summary>
        public int DailyCalories { get; set; }

        /// <summary>
        /// O objetivo de peso do usuário, como manter, ganhar ou perder peso.
        /// </summary>
        public GoalEnum WeightGoal { get; set; }

        /// <summary>
        /// Construtor da resposta da meta de calorias diárias no sistema.
        /// </summary>
        /// <param name="dailyCalories">Meta de calorias diárias.</param>
        /// <param name="weightGoal">O objetivo de peso do usuário.</param>
        public DailyCaloriesResponse(int dailyCalories, GoalEnum weightGoal)
        {
            DailyCalories = dailyCalories;
            WeightGoal = weightGoal;
        }
    }
}
