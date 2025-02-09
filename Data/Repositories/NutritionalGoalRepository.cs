using Dapper;
using MyFood.Data.Repositories.Interfaces;
using MyFood.DTOs.Responses;
using MyFood.Models;
using MyFood.Models.Enums;

namespace MyFood.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de meta nutricional.
    /// </summary>
    public class NutritionalGoalRepository : INutritionalGoalRepository
    {
        private readonly DbSession _dbSession;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para gerenciar a conexão com o banco de dados.
        /// </summary>
        /// <param name="dbSession">Sessão do banco de dados utilizada para execução de consultas e comandos.</param>
        public NutritionalGoalRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        /// <summary>
        /// Registra uma nova meta nutricional na tabela <c>nutritional_goal</c>
        /// </summary>
        /// <param name="nutritionalGoal">Dados cadastrais da meta nutricional.</param>
        /// <returns></returns>
        public async Task CreateNutritionalGoalAsync(NutritionalGoal nutritionalGoal)
        {
            string query = @"INSERT INTO nutritional_goal
                                (user_id, daily_calories, proteins_percentage, carbs_percentage, fats_percentage, weight_goal) 
                             VALUES
                                (@UserId, @DailyCalories, @ProteinsPercentage, @CarbsPercentage, @FatsPercentage, @WeightGoal)";

            await _dbSession.Connection.ExecuteAsync(query, nutritionalGoal, _dbSession.Transaction);
        }

        /// <summary>
        /// Obtém a meta de peso e calorias diárias consumida pelo usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<DailyCaloriesResponse?> GetDailyCaloriesAsync(int userId)
        {
            string query = @"SELECT 
                                daily_calories AS DailyCalories,
                                weight_goal AS WeightGoal
                            FROM nutritional_goal 
                            WHERE user_id = @UserId";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<DailyCaloriesResponse>(query, new { UserId = userId });
        }

        /// <summary>
        /// Atualiza a meta de peso e calorias diárias consumida pelo usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="dailyCalories">Nova meta de calorias diárias.</param>
        /// <param name="weightGoal">O objetivo de peso do usuário.</param>
        /// <returns></returns>
        public async Task UpdateDailyCaloriesAsync(int userId, int dailyCalories, GoalEnum weightGoal)
        {
            string query = @"UPDATE nutritional_goal SET 
                                 daily_calories = @DailyCalories,
                                 weight_goal = @WeightGoal
                            WHERE 
                                user_id = @UserId";

            await _dbSession.Connection.ExecuteAsync(query, new { UserId = userId, DailyCalories = dailyCalories, WeightGoal = weightGoal }, _dbSession.Transaction);
        }

        /// <summary>
        /// Obtém a meta percentual de macronutrientes consumidos diariamente pelo usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<MacrosPercentageResponse?> GetMacrosAsync(int userId)
        {
            string query = @"SELECT 
                                proteins_percentage AS ProteinsPercentage,
                                carbs_percentage AS CarbsPercentage,
                                fats_percentage AS FatsPercentage
                            FROM nutritional_goal 
                            WHERE user_id = @UserId";

            return await _dbSession.Connection.QueryFirstOrDefaultAsync<MacrosPercentageResponse>(query, new { UserId = userId });
        }

        /// <summary>
        /// Atualiza a meta percentual de macronutrientes consumidos diariamente pelo usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="proteinsPercentage">Meta de proteínas diárias consumidas em porcentagem.</param>
        /// <param name="carbsPercentage">Meta de carboidratos diárias consumidas em porcentagem.</param>
        /// <param name="fatsPercentage">Meta de gorduras diárias consumidas em porcentagem.</param>
        /// <returns></returns>
        public async Task UpdateMacrosAsync(int userId, int proteinsPercentage, int carbsPercentage, int fatsPercentage)
        {
            string query = @"UPDATE nutritional_goal SET 
                                proteins_percentage = @ProteinsPercentage,
                                carbs_percentage = @CarbsPercentage,
                                fats_percentage = @FatsPercentage 
                            WHERE 
                                user_id = @UserId";

            await _dbSession.Connection.ExecuteAsync(query, new
            {
                UserId = userId,
                ProteinsPercentage = proteinsPercentage,
                CarbsPercentage = carbsPercentage,
                FatsPercentage = fatsPercentage
            }, _dbSession.Transaction);
        }

        /// <summary>
        /// Verifica se a meta nutricional existe para o usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>Valor booleano indicando se a meta nutricional existe (true) ou não (false).</returns>
        public async Task<bool> NutritionalGoalExistsAsync(int userId)
        {
            string query = @"SELECT 1 FROM nutritional_goal WHERE user_id = @UserId";

            var result = await _dbSession.Connection.ExecuteScalarAsync<int?>(query, new { UserId = userId });

            return result.HasValue;
        }
    }
}
