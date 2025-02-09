using MyFood.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFood.Models
{
    /// <summary>
    /// Representa um usuário no sistema, contendo informações essenciais
    /// para autenticação, autorização e rastreamento de suas refeições.
    /// </summary>
    [Table("user")]
    public class User
    {
        /// <summary>
        /// Identificador único do usuário no banco de dados.
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do usuário.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Endereço de email do usuário.
        /// </summary>
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// Hash da senha do usuário para garantir segurança no armazenamento.
        /// </summary>
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Data e hora de criação do usuário no sistema (em UTC).
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gênero do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        [Column("gender")]
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Idade do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        [Column("age")]
        public int Age { get; set; }

        /// <summary>
        /// Altura do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        [Column("height")]
        public double Height { get; set; }

        /// <summary>
        /// Peso do usuário para cálculos de Taxa Metabólica Basal.
        /// </summary>
        [Column("weight")]
        public double Weight { get; set; }

        /// <summary>
        /// Nível de Atividade para ajustar o gasto calórico diário com base no estilo de vida.
        /// </summary>
        [Column("activity_level")]
        public ActivityLevelEnum ActivityLevel { get; set; }

        /// <summary>
        /// Contrutor da entidade User para registrar novo usuário.
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">Endereço de email do usuário.</param>
        /// <param name="gender">Gênero do usuário.</param>
        /// <param name="age">Idade do usuário.</param>
        /// <param name="height">Altura do usuário.</param>
        /// <param name="weight">Peso do usuário.</param>
        /// <param name="activityLevel">Nível de Atividade do usuário.</param>
        /// <param name="passwordHash">Hash da senha do usuário.</param>
        public User(string name, string email, GenderEnum gender, int age, int height, int weight, ActivityLevelEnum activityLevel, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            Gender = gender;
            Age = age;
            Height = height;
            Weight = weight;
            ActivityLevel = activityLevel;
        }

        /// <summary>
        /// Contrutor da entidade User para o login do usuário.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">Email do usuário.</param>
        /// <param name="passwordHash">Hash da senha do usuário.</param>
        public User(int id, string name, string email, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}
