using Dapper;
using MyFood.Models;
using MyFood.Data.Repositories.Interfaces;

namespace MyFood.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de usuários.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DbSession _dbSession;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para gerenciar a conexão com o banco de dados.
        /// </summary>
        /// <param name="dbSession"></param>
        public UserRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        /// <summary>
        /// Registra um novo usuário na tabela <c>user</c>
        /// </summary>
        /// <param name="user">Informações do usuário a serem registradas.</param>
        public async Task CreateAsync(User user)
        {
            string query = @"INSERT INTO ""user"" (name, email, password_hash, created_at) 
                             VALUES (@Name, @Email, @PasswordHash, @CreatedAt)";

            await _dbSession.Connection.ExecuteAsync(query, user, _dbSession.Transaction);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string query = "SELECT * FROM \"user\"";

            var result = await _dbSession.Connection.QueryAsync<User>(query);
            return result.ToList();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM \"user\" WHERE id = @Id";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task<bool> UpdateAsync(User food)
        {
            string query = @"UPDATE ""user"" SET 
                                 name = @Name, 
                                 email = @Email, 
                                 password_hash = @PasswordHash
                             WHERE id = @Id";

            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, food, _dbSession.Transaction);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string query = "DELETE FROM \"user\" WHERE id = @Id";

            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, new { Id = id }, _dbSession.Transaction);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Obtém o usuário a partir do email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns>Usuário correspondente.</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            string query = @"SELECT 
                                name AS Name,
                                email AS Email,
                                password_hash AS PasswordHash
                            FROM ""user""
                            WHERE email = @Email";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }
    }
}
