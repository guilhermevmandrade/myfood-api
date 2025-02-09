using MyFood.Data.Repositories.Interfaces;
using MyFood.Data;
using MyFood.Models;
using MyFood.Services.Interfaces;
using MyFood.DTOs.Responses;
using MyFood.DTOs.Requests;

namespace MyFood.Services
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MealService(IMealRepository mealRepository, IUnitOfWork unitOfWork)
        {
            _mealRepository = mealRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registra uma nova refeição.
        /// </summary>
        /// <param name="request">Dados cadastrais da refeição.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task RegisterMealAsync(MealRequest request, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                var meal = new Meal(userId, request.Description, request.MealTime);

                await _mealRepository.CreateAsync(meal);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Lista todas refeições vinculadas a um usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<List<MealResponse>> ListUserMealAsync(int userId)
        {
            var mealList = await _mealRepository.GetAllByUserIdAsync(userId);

            return mealList.ToList();
        }

        /// <summary>
        /// Obtém os principais dados da refeição de um usuário.
        /// </summary>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<MealResponse> GetUserMealAsync(int mealId, int userId)
        {
            var meal = await _mealRepository.GetUserMealByIdAsync(mealId, userId);
            if (meal == null)
            {
                throw new Exception("Refeição não encontrada.");
            }

            return meal;
        }

        /// <summary>
        /// Atualiza os dados da refeição.
        /// </summary>
        /// <param name="request">Novos dados da refeição a ser atualizada.</param>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task UpdateMealAsync(MealRequest request, int mealId, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool mealExists = await _mealRepository.MealExistsAsync(mealId, userId);
                if (!mealExists)
                {
                    throw new Exception("Refeição não encontrada.");
                }

                await _mealRepository.UpdateAsync(request, mealId);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Deleta a refeição de um usuário pelo seu identificador.
        /// </summary>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteMealAsync(int mealId, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool mealExists = await _mealRepository.MealExistsAsync(mealId, userId);
                if (!mealExists)
                {
                    throw new Exception("Refeição não encontrada.");
                }

                await _mealRepository.DeleteAsync(mealId);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Adiciona um alimento a uma refeição do usuário no banco de dados.
        /// </summary>
        /// <param name="request">Dados do alimento, como quantidade e unidade de medida, na refeição.</param>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task AddFoodToMealAsync(MealFoodRequest request, int mealId, int foodId, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool mealExists = await _mealRepository.MealExistsAsync(mealId, userId);
                if (!mealExists)
                {
                    throw new Exception("Refeição não encontrada.");
                }

                await _mealRepository.AddFoodToMealAsync(mealId, foodId, request.Quantity, request.Unit);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Remove o alimento de uma refeição no banco de dados.
        /// </summary>
        /// <param name="mealId">Identificador da refeição.</param>
        /// <param name="foodId">Identificador do alimento.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task RemoveFoodFromMealAsync(int mealId, int foodId, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool mealExists = await _mealRepository.MealExistsAsync(mealId, userId);
                if (!mealExists)
                {
                    throw new Exception("Refeição não encontrada.");
                }

                await _mealRepository.RemoveFoodFromMealAsync(mealId, foodId);

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
