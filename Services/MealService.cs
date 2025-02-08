using MyFood.Data.Repositories.Interfaces;
using MyFood.Data;
using MyFood.Models;
using MyFood.Services.Interfaces;
using MyFood.DTOs.Responses;
using MyFood.DTOs.Requests;
using MyFood.Data.Repositories;

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

                var meal = await _mealRepository.GetUserMealByIdAsync(mealId, userId);
                if (meal == null)
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

                var meal = await _mealRepository.GetUserMealByIdAsync(mealId, userId);
                if (meal == null)
                {
                    throw new Exception("Alimento não encontrado.");
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
    }
}
