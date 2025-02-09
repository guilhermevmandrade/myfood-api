using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class NutritionalGoalRequestValidator : AbstractValidator<NutritionalGoalRequest>
    {
        public NutritionalGoalRequestValidator()
        {
            RuleFor(x => x.DailyCalories)
                .GreaterThan(0).WithMessage("A meta de calorias diárias deve ser maior que zero.")
                .LessThanOrEqualTo(10000).WithMessage("A meta de calorias diárias não pode ultrapassar 10.000 calorias.");

            RuleFor(x => x.ProteinsPercentage)
                .InclusiveBetween(0, 100).WithMessage("O percentual de proteínas deve estar entre 0 e 100.");

            RuleFor(x => x.CarbsPercentage)
                .InclusiveBetween(0, 100).WithMessage("O percentual de carboidratos deve estar entre 0 e 100.");

            RuleFor(x => x.FatsPercentage)
                .InclusiveBetween(0, 100).WithMessage("O percentual de gorduras deve estar entre 0 e 100.");

            RuleFor(x => x)
                .Must(x => x.ProteinsPercentage + x.CarbsPercentage + x.FatsPercentage == 100)
                .WithMessage("A soma dos percentuais de proteínas, carboidratos e gorduras deve ser igual a 100.");

            RuleFor(x => x.WeightGoal)
               .IsInEnum().WithMessage("O objetivo deve ser um valor válido.");
        }
    }

}
