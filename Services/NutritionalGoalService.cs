using MyFood.Data.Repositories.Interfaces;
using MyFood.Data;
using MyFood.DTOs.Requests;
using MyFood.DTOs.Responses;
using MyFood.Services.Interfaces;
using MyFood.Models;
using MyFood.Models.Enums;

namespace MyFood.Services
{
    /// <summary>
    /// Implementação do serviço de metas nutricionais, responsável pelo registro, atualização, busca e exclusão.
    /// </summary>
    public class NutritionalGoalService : INutritionalGoalService
    {
        private readonly INutritionalGoalRepository _nutritionalGoalRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Construtor que recebe as dependências necessárias para manipulação de metas nutricionais.
        /// </summary>
        public NutritionalGoalService(INutritionalGoalRepository nutritionalGoalRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _nutritionalGoalRepository = nutritionalGoalRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Registra uma nova meta nutricional.
        /// </summary>
        /// <param name="request">Dados cadastrais da meta nutricional.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task DefineNutritionalGoalAsync(NutritionalGoalRequest request, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool goalExists = await _nutritionalGoalRepository.NutritionalGoalExistsAsync(userId);
                if (goalExists)
                {
                    throw new Exception("Meta nutricional já registrada.");
                }

                var nutritionalGoal = new NutritionalGoal
                    (userId, request.DailyCalories, request.ProteinsPercentage, request.CarbsPercentage, request.FatsPercentage, request.WeightGoal);

                await _nutritionalGoalRepository.CreateNutritionalGoalAsync(nutritionalGoal);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Obtém a meta de calorias diárias consumida pelo usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<DailyCaloriesResponse> GetDailyCaloriesAsync(int userId)
        {
            var dailyCalories = await _nutritionalGoalRepository.GetDailyCaloriesAsync(userId);
            if (dailyCalories == null)
            {
                throw new Exception("Meta nutricional não encontrada.");
            }

            return dailyCalories;
        }

        /// <summary>
        /// Atualiza a meta de calorias diárias consumida pelo usuário.
        /// </summary>
        /// <param name="request">Novos dados de calorias diárias.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task UpdateDailyCaloriesAsync(DailyCaloriesRequest request, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool goalExists = await _nutritionalGoalRepository.NutritionalGoalExistsAsync(userId);
                if (!goalExists)
                {
                    throw new Exception("Meta nutricional não encontrada.");
                }

                await _nutritionalGoalRepository.UpdateDailyCaloriesAsync(userId, request.DailyCalories, request.WeightGoal);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Lista a meta percentual de macronutrientes consumidos diaramente pelo usuário.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task<MacrosPercentageResponse> GetMacrosAsync(int userId)
        {
            var macros = await _nutritionalGoalRepository.GetMacrosAsync(userId);
            if (macros == null)
            {
                throw new Exception("Meta nutricional não encontrada.");
            }

            return macros;
        }

        /// <summary>
        /// Atualiza a meta percentual de macronutrientes consumidos diaramente pelo usuário.
        /// </summary>
        /// <param name="request">Novos dados de macronutrientes percentuais diários.</param>
        /// <param name="userId">Identificador do usuário.</param>
        /// <returns></returns>
        public async Task UpdateMacrosAsync(MacrosPercentageRequest request, int userId)
        {
            try
            {
                _unitOfWork.BeginTransaction();

                bool goalExists = await _nutritionalGoalRepository.NutritionalGoalExistsAsync(userId);
                if (!goalExists)
                {
                    throw new Exception("Meta nutricional não encontrada.");
                }

                await _nutritionalGoalRepository.UpdateMacrosAsync(userId, request.ProteinsPercentage, request.CarbsPercentage, request.FatsPercentage);

                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Usa a equação de Mifflin-St Jeor para sugerir a quantidade diária de calorias com base nas características do usuário e seu objetivo de peso.
        /// </summary>
        /// <param name="userId">Identificador do usuário.</param>
        /// <param name="weightGoal">Objetivo de peso do usuário (perder, manter ou ganhar peso).</param>
        /// <returns></returns>
        public async Task<DailyCaloriesResponse> SuggestDailyCaloriesAsync(int userId, GoalEnum weightGoal)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            double bmr;
            if (user.Gender == GenderEnum.Male)
            {
                bmr = 10 * user.Weight + 6.25 * user.Height - 5 * user.Age + 5;
            }
            else
            {
                bmr = 10 * user.Weight + 6.25 * user.Height - 5 * user.Age - 161;
            }

            double maintenanceCalories = bmr * GetActivityMultiplier(user.ActivityLevel);
            int dailyCalories = (int)(maintenanceCalories * GetGoalMultiplier(weightGoal));

            return new DailyCaloriesResponse(dailyCalories, weightGoal);
        }

        /// <summary>
        /// Obtém o multiplicador de calorias com base no nível de atividade do usuário.
        /// </summary>
        /// <param name="activityLevel">Nível de atividade do usuário.</param>
        /// <returns>Fator multiplicador para ajuste da TMB.</returns>
        private static double GetActivityMultiplier(ActivityLevelEnum activityLevel)
        {
            return activityLevel switch
            {
                ActivityLevelEnum.Sedentary => 1.2,
                ActivityLevelEnum.Light => 1.375,
                ActivityLevelEnum.Moderate => 1.55,
                ActivityLevelEnum.Active => 1.725,
                ActivityLevelEnum.VeryActive => 1.9,
                _ => 1.2
            };
        }

        /// <summary>
        /// Obtém o multiplicador de calorias com base no objetivo de peso do usuário.
        /// </summary>
        /// <param name="goal">Objetivo de peso do usuário.</param>
        /// <returns>Fator multiplicador para ajuste da ingestão calórica.</returns>
        private static double GetGoalMultiplier(GoalEnum goal)
        {
            return goal switch
            {
                GoalEnum.LoseWeight => 0.85,
                GoalEnum.MaintainWeight => 1.0, 
                GoalEnum.GainWeight => 1.15,
                _ => 1.0
            };
        }
    }
}
