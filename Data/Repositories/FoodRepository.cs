using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using MyFood.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyFood.Data.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly DbSession _dbSession;

        public FoodRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<int> CreateAsync(Food food)
        {
            string query = @"INSERT INTO foods (name, calories, protein, carbs, fat) 
                             VALUES (@Name, @Calories, @Protein, @Carbs, @Fat) 
                             RETURNING id";
            return await _dbSession.Connection.ExecuteScalarAsync<int>(query, food, _dbSession.Transaction);
        }

        public async Task<IEnumerable<Food>> GetAllAsync()
        {
            string query = "SELECT * FROM foods";
            var result = await _dbSession.Connection.QueryAsync<Food>(query);
            return result.ToList();
        }

        public async Task<Food?> GetByIdAsync(int id)
        {
            string query = "SELECT * FROM foods WHERE id = @Id";
            return await _dbSession.Connection.QueryFirstOrDefaultAsync<Food>(query, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Food food)
        {
            string query = @"UPDATE foods SET 
                                 name = @Name, 
                                 calories = @Calories, 
                                 protein = @Protein, 
                                 carbs = @Carbs, 
                                 fat = @Fat 
                             WHERE id = @Id";
            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, food, _dbSession.Transaction);
            return rowsAffected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string query = "DELETE FROM foods WHERE id = @Id";
            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, new { Id = id }, _dbSession.Transaction);
            return rowsAffected > 0;
        }
    }
}
