namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição para registrar e atualizar uma refeição.
    /// </summary>
    public class MealRequest
    {
        /// <summary>
        /// Descrição textual da refeição.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Data e horário da refeição.
        /// </summary>
        public DateTime MealTime { get; set; }
    }
}
