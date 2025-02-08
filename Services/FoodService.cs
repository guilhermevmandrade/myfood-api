using Azure.Core;
using MyFood.Data.Repositories.Interfaces;
using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Models;
using MyFood.Services.Interfaces;

namespace MyFood.Services
{
    /// <summary>
    /// Implementação do serviço de alimentos, responsável pelo registro, atualização e busca.
    /// </summary>
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para manipulação de alimentos.
        /// </summary>
        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        /// <summary>
        /// Lista todos alimentos vinculados a um usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<List<FoodResponse>> ListUserFoodAsync(int userId)
        {
            var foodList = await _foodRepository.GetAllByUserIdAsync(userId);

            return foodList.ToList();
        }

        /// <summary>
        /// Obtém os principais dados do alimento de um usuário.
        /// </summary>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<FoodResponse> GetUserFoodAsync(int foodId, int userId)
        {
            var food = await _foodRepository.GetUserFoodById(foodId, userId);
            if (food == null) 
            {
                throw new Exception("Alimento não encontrado.");
            }

            var foodResponse = new FoodResponse(food.Id, food.Name, food.Calories, food.Proteins, food.Carbs, food.Fats);

            return foodResponse;
        }

        /// <summary>
        /// Registra um novo alimento.
        /// </summary>
        /// <param name="request">Dados cadastrais do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task RegisterFoodAsync(FoodRequest request, int userId)
        {
            var food = new Food(userId, request.Name, request.Calories, request.Proteins, request.Carbs, request.Fats);

            await _foodRepository.CreateAsync(food);
        }

        /// <summary>
        /// Atualiza os dados do alimento.
        /// </summary>
        /// <param name="request">Novos dados do alimento a ser atualizado.</param>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task UpdateFoodAsync(FoodRequest request, int foodId, int userId)
        {
            var food = await _foodRepository.GetUserFoodById(foodId, userId);
            if (food == null)
            {
                throw new Exception("Alimento não encontrado.");
            }

            await _foodRepository.UpdateAsync(request, foodId);
        }

        /// <summary>
        /// Deleta o alimento de um usuário pelo seu identificador.
        /// </summary>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task DeleteFoodAsync(int foodId, int userId)
        {
            var food = await _foodRepository.GetUserFoodById(foodId, userId);
            if (food == null)
            {
                throw new Exception("Alimento não encontrado.");
            }

            await _foodRepository.DeleteAsync(foodId);
        }
    }
}
