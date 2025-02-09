using Dapper;
using MyFood.Data.Repositories.Interfaces;
using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models;

namespace MyFood.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de alimentos.
    /// </summary>
    public class FoodRepository : IFoodRepository
    {
        private readonly DbSession _dbSession;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para gerenciar a conexão com o banco de dados.
        /// </summary>
        /// <param name="dbSession">Sessão do banco de dados utilizada para execução de consultas e comandos.</param>
        public FoodRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        /// <summary>
        /// Registra um novo alimento na tabela <c>food</c>
        /// </summary>
        /// <param name="food">Dados do alimento para cadastro.</param>
        /// <returns></returns>
        public async Task CreateAsync(Food food)
        {
            string query = @"INSERT INTO food 
                                (user_id, name, calories, proteins, carbs, fats) 
                             VALUES 
                                (@UserId, @Name, @Calories, @Proteins, @Carbs, @Fats)";

            await _dbSession.Connection.ExecuteAsync(query, food, _dbSession.Transaction);
        }

        /// <summary>
        /// Obtém todos alimentos de um usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>Uma coleção contendo os alimentos e suas informações nutricionais.</returns>
        public async Task<IEnumerable<FoodResponse>> GetAllByUserIdAsync(int userId)
        {
            string query = @"SELECT 
                                id AS FoodId,
                                name AS Name,
                                calories AS Calories,
                                proteins AS Proteins,
                                carbs AS Carbs,
                                fats AS Fats
                            FROM food
                            WHERE user_id = @UserId";

            var result = await _dbSession.Connection.QueryAsync<FoodResponse>(query, new { UserId = userId });
            return result.ToList();
        }

        /// <summary>
        /// Obtém os dados de um alimento de um usuário.
        /// </summary>
        /// <param name="id">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>O alimento e suas informações nutricionais.</returns>
        public async Task<FoodResponse?> GetUserFoodByIdAsync(int id, int userId)
        {
            string query = @"SELECT 
                                id AS FoodId,
                                name AS Name,
                                calories AS Calories,
                                proteins AS Proteins,
                                carbs AS Carbs,
                                fats AS Fats
                            FROM food
                            WHERE id = @Id AND user_id = @UserId";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<FoodResponse>(query, new { Id = id, UserId = userId });
        }

        /// <summary>
        /// Atualiza os dados do alimento
        /// </summary>
        /// <param name="food">Dados do alimento a ser atualizado.</param>
        /// <param name="id">Identificador do alimento.</param>
        /// <returns></returns>
        public async Task UpdateAsync(FoodRequest food, int id)
        {
            string query = @"UPDATE food SET 
                                 name = @Name, 
                                 calories = @Calories, 
                                 proteins = @Proteins, 
                                 carbs = @Carbs, 
                                 fats = @Fats
                             WHERE id = @Id";

            await _dbSession.Connection.ExecuteAsync(query, new { food.Name, food.Calories, food.Proteins, food.Carbs, food.Fats, Id = id }, _dbSession.Transaction);
        }


        /// <summary>
        /// Deleta o alimento a partir do seu identificador.
        /// </summary>
        /// <param name="id">Identificador do alimento.</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM food WHERE id = @Id";

            await _dbSession.Connection.ExecuteAsync(query, new { Id = id }, _dbSession.Transaction);
        }

        /// <summary>
        /// Verifica se um alimento com o identificador especificado existe para o usuário.
        /// </summary>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>Valor booleano indicando se o alimento existe para (true) ou não (false).</returns>
        public async Task<bool> FoodExistsAsync(int foodId, int userId)
        {
            string query = @"SELECT 1 FROM food 
                            WHERE 
                                id = @FoodId 
                            AND 
                                user_id = @UserId";

            var result = await _dbSession.Connection.ExecuteScalarAsync<int?>(query, new { FoodId = foodId, UserId = userId });

            return result.HasValue;
        }
    }
}
