namespace MyFood.DTOs.Responses
{
    /// <summary>
    /// Representa a resposta da requisição para listar os dados de um alimento.
    /// </summary>
    public class FoodResponse
    {
        /// <summary>
        /// Identificador único do alimento no banco de dados.
        /// </summary>
        public int FoodId { get; set; }

        /// <summary>
        /// Nome do alimento.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Quantidade de calorias por porção do alimento.
        /// </summary>
        public decimal Calories { get; set; }

        /// <summary>
        /// Quantidade de proteína (em gramas) por porção do alimento.
        /// </summary>
        public decimal Proteins { get; set; }

        /// <summary>
        /// Quantidade de carboidratos (em gramas) por porção do alimento.
        /// </summary>
        public decimal Carbs { get; set; }

        /// <summary>
        /// Quantidade de gorduras (em gramas) por porção do alimento.
        /// </summary>
        public decimal Fats { get; set; }

        /// <summary>
        /// Construtor que representa a resposta de um alimento cadastrado no sistema.
        /// </summary>
        /// <param name="foodid">Identificador único do alimento.</param>
        /// <param name="name">Nome do alimento.</param>
        /// <param name="calories">Quantidade de calorias por porção do alimento.</param>
        /// <param name="proteins">Quantidade de proteína (em gramas) por porção do alimento.</param>
        /// <param name="carbs">Quantidade de carboidratos (em gramas) por porção do alimento.</param>
        /// <param name="fats">Quantidade de gorduras (em gramas) por porção do alimento.</param>
        public FoodResponse(int foodid, string name, decimal calories, decimal proteins, decimal carbs, decimal fats)
        {
            FoodId = foodid;
            Name = name;
            Calories = calories;
            Proteins = proteins;
            Carbs = carbs;
            Fats = fats;
        }
    }
}
