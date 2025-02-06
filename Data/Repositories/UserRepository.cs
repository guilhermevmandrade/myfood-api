using Dapper;
using MyFood.Models;
using MyFood.Data.Repositories.Interfaces;

namespace MyFood.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbSession _dbSession;

        public UserRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<int> CreateAsync(User user)
        {
            string query = @"INSERT INTO users (name, email, password_hash, created_at) 
                             VALUES (@Name, @Email, @PasswordHash, @CreatedAt) 
                             RETURNING id";
            return await _dbSession.Connection.ExecuteScalarAsync<int>(query, user, _dbSession.Transaction);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            string query = "SELECT * FROM users";
            var result = await _dbSession.Connection.QueryAsync<User>(query);
            return result.ToList();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM users WHERE id = @Id";
            return await _dbSession.Connection.QueryFirstOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task<bool> UpdateAsync(User food)
        {
            string query = @"UPDATE users SET 
                                 name = @Name, 
                                 email = @Email, 
                                 password_hash = @PasswordHash
                             WHERE id = @Id";
            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, food, _dbSession.Transaction);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string query = "DELETE FROM users WHERE id = @Id";
            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, new { Id = id }, _dbSession.Transaction);
            return rowsAffected > 0;
        }

        /// <summary>
        /// Obtém o usuário a partir do email.
        /// </summary>
        /// <param name="email">Email do usuário.</param>
        /// <returns></returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            string query = "SELECT * FROM users WHERE email = @Email";
            return await _dbSession.Connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }
    }
}
