namespace MyFood.DTOs.Responses
{
    /// <summary>
    /// Representa a resposta da requisição para listar a meta percentual de macronutrientes consumidos diaramente.
    /// </summary>
    public class MacrosPercentageResponse
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

        /// <summary>
        /// Construtor da resposta da meta percentual de macronutrientes no sistema.
        /// </summary>
        /// <param name="proteinsPercentage">Percentual de calorias vindas da proteína.</param>
        /// <param name="carbsPercentage">Percentual de calorias vindas dos carboidratos.</param>
        /// <param name="fatsPercentage">Percentual de calorias vindas das gorduras.</param>
        public MacrosPercentageResponse(int proteinsPercentage, int carbsPercentage, int fatsPercentage)
        {
            ProteinsPercentage = proteinsPercentage;
            CarbsPercentage = carbsPercentage;
            FatsPercentage = fatsPercentage;
        }
    }
}
