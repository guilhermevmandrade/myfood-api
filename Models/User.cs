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
        /// Contrutor da entidade User
        /// </summary>
        /// <param name="name">Nome do usuário.</param>
        /// <param name="email">Email do usuário.</param>
        /// <param name="passwordHash">Hash da senha do usuário.</param>
        public User(string name, string email, string passwordHash)
        {
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;  
        }
    }
}
