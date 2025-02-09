using Dapper;
using MyFood.Models;
using MyFood.Data.Repositories.Interfaces;
using MyFood.DTOs.Responses;
using MyFood.DTOs.Requests;
using MyFood.Models.Enums;

namespace MyFood.Data.Repositories
{
    /// <summary>
    /// Implementação do repositório de refeições.
    /// </summary>
    public class MealRepository : IMealRepository
    {
        private readonly DbSession _dbSession;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para gerenciar a conexão com o banco de dados.
        /// </summary>
        /// <param name="dbSession">Sessão do banco de dados utilizada para execução de consultas e comandos.</param>
        public MealRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        /// <summary>
        /// Registra uma nova refeição na tabela <c>meal</c>
        /// </summary>
        /// <param name="meal">Dados da refeição para cadastro.</param>
        /// <returns></returns>
        public async Task CreateAsync(Meal meal)
        {
            string query = @"INSERT INTO meal
                                (user_id, description, meal_time) 
                             VALUES
                                (@UserId, @Description, @MealTime)";

            await _dbSession.Connection.ExecuteAsync(query, meal, _dbSession.Transaction);
        }

        /// <summary>
        /// Obtém todas as refeições de um usuário com seus respectivos alimentos e informações nutricionais.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>Uma coleção contendo as refeições e seus alimentos.</returns>
        public async Task<IEnumerable<MealResponse>> GetAllByUserIdAsync(int userId)
        {
            string query = @"SELECT 
                                m.id AS MealId,
                                m.description AS Description,
                                m.meal_time AS MealTime,
                                mf.id AS MealFoodId,
                                mf.quantity AS Quantity,
                                mf.unit AS Unit,
                                f.id AS FoodId,
                                f.name AS Name,
                                f.calories AS Calories,
                                f.proteins AS Proteins,
                                f.carbs AS Carbs,
                                f.fats AS Fats
                            FROM meal m
                            LEFT JOIN meal_food mf ON m.id = mf.meal_id
                            LEFT JOIN food f ON mf.food_id = f.id
                            WHERE m.user_id = @UserId";

            var mealDictionary = new Dictionary<int, MealResponse>();

            var result = await _dbSession.Connection.QueryAsync<MealResponse, MealFoodResponse, FoodResponse, MealResponse>(
                query, (meal, mealFood, food) =>
                {
                    // Verifica se a refeição já foi registrada no mealDictionary.Se já estiver, a refeição (mealEntry) será usada.
                    // Caso contrário, ela é criada, e uma nova lista de MealFoodResponse é inicializada para armazenar os alimentos associados a essa refeição.
                    if (!mealDictionary.TryGetValue(meal.MealId, out var mealEntry))
                    {
                        mealEntry = meal;
                        mealEntry.MealFoods = new List<MealFoodResponse>();
                        mealDictionary[meal.MealId] = mealEntry;
                    }

                    // Adiciona cada alimento associado à refeição,
                    // garantindo que a lista de alimentos da refeição seja preenchida corretamente.
                    if (food != null)
                    {
                        mealFood.Food = food;
                    }
                    mealEntry.MealFoods.Add(mealFood);
                    return mealEntry;
                },
                new { UserId = userId },
                splitOn: "MealFoodId,FoodId"
            );

            return mealDictionary.Values.ToList();
        }

        /// <summary>
        /// Obtém os dados de uma refeição do usuário.
        /// </summary>
        /// <param name="id">Identificador da refeição.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>A refeição e seus alimentos.</returns>
        public async Task<MealResponse?> GetUserMealByIdAsync(int id, int userId)
        {
            string query = @"SELECT 
                                m.id AS MealId,
                                m.description AS Description,
                                m.meal_time AS MealTime,
                                mf.id AS MealFoodId,
                                mf.quantity AS Quantity,
                                mf.unit AS Unit,
                                f.id AS FoodId,
                                f.name AS Name,
                                f.calories AS Calories,
                                f.proteins AS Proteins,
                                f.carbs AS Carbs,
                                f.fats AS Fats
                            FROM meal m
                            LEFT JOIN meal_food mf ON m.id = mf.meal_id
                            LEFT JOIN food f ON mf.food_id = f.id
                            WHERE m.user_id = @UserId AND m.id = @Id";

            var mealDictionary = new Dictionary<int, MealResponse>();

            var result = await _dbSession.Connection.QueryAsync<MealResponse, MealFoodResponse, FoodResponse, MealResponse>(
                query, (meal, mealFood, food) =>
                {
                    // Verifica se a refeição já foi registrada no mealDictionary.Se já estiver, a refeição (mealEntry) será usada.
                    // Caso contrário, ela é criada, e uma nova lista de MealFoodResponse é inicializada para armazenar os alimentos associados a essa refeição.
                    if (!mealDictionary.TryGetValue(meal.MealId, out var mealEntry))
                    {
                        mealEntry = meal;
                        mealEntry.MealFoods = new List<MealFoodResponse>();
                        mealDictionary[meal.MealId] = mealEntry;
                    }

                    // Adiciona cada alimento associado à refeição,
                    // garantindo que a lista de alimentos da refeição seja preenchida corretamente.
                    if (food != null)
                    {
                        mealFood.Food = food;
                    }
                    mealEntry.MealFoods.Add(mealFood);
                    return mealEntry;
                },
                new { Id = id, UserId = userId },
                splitOn: "MealFoodId,FoodId"
            );

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Atualiza os dados da refeição.
        /// </summary>
        /// <param name="meal">Dados da refeição a ser atualizada.</param>
        /// <param name="id">Identificador da refeição.</param>
        /// <returns></returns>
        public async Task UpdateAsync(MealRequest meal, int id)
        {
            string query = @"UPDATE meal SET  
                                 description = @Description, 
                                 meal_time = @MealTime
                             WHERE id = @Id";
            
            await _dbSession.Connection.ExecuteAsync(query, new { meal.Description, meal.MealTime, Id = id }, _dbSession.Transaction);
        }

        /// <summary>
        /// Deleta a refeição a partir de seu identificador.
        /// </summary>
        /// <param name="id">Identificador da refeição.</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            string query = "DELETE FROM meal WHERE id = @Id";
            
            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, new { Id = id }, _dbSession.Transaction);
        }

        /// <summary>
        /// Verifica se uma refeição com o identificador especificado existe para o usuário.
        /// </summary>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns>Valor booleano indicando se a refeição existe para o usuário (true) ou não (false).</returns>
        public async Task<bool> MealExistsAsync(int mealId, int userId)
        {
            string query = @"SELECT 1 FROM meal 
                            WHERE 
                                id = @MealId 
                            AND 
                                user_id = @UserId";

            var result = await _dbSession.Connection.ExecuteScalarAsync<int?>(query, new { MealId = mealId, UserId = userId });

            return result.HasValue;
        }

        /// <summary>
        /// Adiciona um alimento a uma refeição do usuário no banco de dados.
        /// </summary>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="quantity">Quantidade do alimento na refeição.</param>
        /// <param name="unit">Unidade de medida da quantidade de alimento.</param>
        /// <returns></returns>
        public async Task AddFoodToMealAsync(int mealId, int foodId, decimal quantity, MeasurementUnit unit)
        {
            string query = @"INSERT INTO meal_food
                                (meal_id, food_id, quantity, unit) 
                             VALUES
                                (@MealId, @FoodId, @Quantity, @Unit)";

            await _dbSession.Connection.ExecuteAsync(query, new { MealId = mealId, FoodId = foodId, Quantity = quantity, Unit = unit}, _dbSession.Transaction);
        }

        /// <summary>
        /// Remove um alimento de uma refeição no banco de dados.
        /// </summary>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <returns></returns>
        public async Task RemoveFoodFromMealAsync(int mealId, int foodId)
        {
            string query = @"DELETE FROM meal_food 
                            WHERE 
                                meal_id = @MealId 
                            AND 
                                food_id = @FoodId";

            var rowsAffected = await _dbSession.Connection.ExecuteAsync(query, new { MealId = mealId, FoodId = foodId }, _dbSession.Transaction);
        }
    }
}
