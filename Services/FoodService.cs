using Azure.Core;
using MyFood.Data;
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
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para manipulação de alimentos.
        /// </summary>
        public FoodService(IFoodRepository foodRepository, IUnitOfWork unitOfWork)
        {
            _foodRepository = foodRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registra um novo alimento.
        /// </summary>
        /// <param name="request">Dados cadastrais do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task RegisterFoodAsync(FoodRequest request, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var food = new Food(userId, request.Name, request.Calories, request.Proteins, request.Carbs, request.Fats);

                await _foodRepository.CreateAsync(food);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
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
        public async Task<FoodResponse> GetUserFoodAsync(int foodId, int userId)
        {
            var food = await _foodRepository.GetUserFoodByIdAsync(foodId, userId);
            if (food == null) 
            {
                throw new Exception("Alimento não encontrado.");
            }

            return food;
        }

        /// <summary>
        /// Atualiza os dados do alimento.
        /// </summary>
        /// <param name="request">Novos dados do alimento a ser atualizado.</param>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task UpdateFoodAsync(FoodRequest request, int foodId, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool foodExists = await _foodRepository.FoodExistsAsync(foodId, userId);
                if (!foodExists)
                {
                    throw new Exception("Alimento não encontrado.");
                }

                await _foodRepository.UpdateAsync(request, foodId);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Deleta o alimento de um usuário pelo seu identificador.
        /// </summary>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task DeleteFoodAsync(int foodId, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool foodExists = await _foodRepository.FoodExistsAsync(foodId, userId);
                if (!foodExists)
                {
                    throw new Exception("Alimento não encontrado.");
                }

                await _foodRepository.DeleteAsync(foodId);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
