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
            string query = @"INSERT INTO users (name, email, passwordHash, createdAt) 
                             VALUES (@Name, @Email, @PasswordHash, @CreatedA) 
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
                                 passwordHash = @PasswordHash
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
    }
}
