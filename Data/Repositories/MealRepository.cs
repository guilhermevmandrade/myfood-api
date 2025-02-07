using Dapper;
using MyFood.Models;
using MyFood.Data.Repositories.Interfaces;

namespace MyFood.Data.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly DbSession _dbSession;

        public MealRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<int> CreateAsync(Meal user)
        {
            string query = @"INSERT INTO meals (userId, description, mealTime) 
                             VALUES (@UserId, @Description, @MealTime) 
                             RETURNING id";
            return await _dbSession.Connection.ExecuteScalarAsync<int>(query, user, _dbSession.Transaction);
        }

        public async Task<IEnumerable<Meal>> GetAllAsync()
        {
            string query = "SELECT * FROM meals";
            var result = await _dbSession.Connection.QueryAsync<Meal>(query);
            return result.ToList();
        }

        public async Task<Meal?> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM meals WHERE id = @Id";
            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Meal>(query, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Meal food)
        {
            string query = @"UPDATE meals SET 
                                 userId = @UserId, 
                                 description = @Description, 
                                 mealTime = @MealTime
                             WHERE id = @Id";
            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, food, _dbSession.Transaction);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string query = "DELETE FROM meals WHERE id = @Id";
            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, new { Id = id }, _dbSession.Transaction);
            return rowsAffected > 0;
        }
    }
}
