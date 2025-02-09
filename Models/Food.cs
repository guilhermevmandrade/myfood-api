using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    /// <summary>
    /// Representa um alimento e suas informações nutricionais.
    /// </summary>
    [Table("food")]
    public class Food
    {
        /// <summary>
        /// Identificador único do alimento no banco de dados.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Identificador do usuário proprietário deste alimento.
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// Nome do alimento.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Quantidade de calorias por porção do alimento.
        /// </summary>
        [Column("calories")]
        public decimal Calories { get; set; }

        /// <summary>
        /// Quantidade de proteína (em gramas) por porção do alimento.
        /// </summary>
        [Column("proteins")]
        public decimal Proteins { get; set; }

        /// <summary>
        /// Quantidade de carboidratos (em gramas) por porção do alimento.
        /// </summary>
        [Column("carbs")]
        public decimal Carbs { get; set; }

        /// <summary>
        /// Quantidade de gorduras (em gramas) por porção do alimento.
        /// </summary>
        [Column("fats")]
        public decimal Fats { get; set; }

        /// <summary>
        /// Contrutor da entidade Food para o cadastro de novos alimentos.
        /// </summary>
        /// <param name="userId">Identificador do usuário proprietário deste alimento.</param>
        /// <param name="name">Nome do alimento.</param>
        /// <param name="calories">Quantidade de calorias por porção do alimento.</param>
        /// <param name="proteins">Quantidade de proteína (em gramas) por porção do alimento.</param>
        /// <param name="carbs">Quantidade de gorduras (em gramas) por porção do alimento.</param>
        /// <param name="fats">Quantidade de gorduras (em gramas) por porção do alimento.</param>
        public Food(int userId, string name, decimal calories, decimal proteins, decimal carbs, decimal fats)
        {
            UserId = userId;
            Name = name;
            Calories = calories;
            Proteins = proteins;
            Carbs = carbs;
            Fats = fats;
        }
    }
}
