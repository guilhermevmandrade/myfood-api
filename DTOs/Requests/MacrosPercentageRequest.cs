namespace MyFood.DTOs.Requests
{
    /// <summary>
    /// Representa a requisição para atualizar a meta percentual de macronutrientes consumidos diariamente.
    /// </summary>
    public class MacrosPercentageRequest
    {
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
    }
}
