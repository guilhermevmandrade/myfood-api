using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class DailyCaloriesRequestValidator : AbstractValidator<DailyCaloriesRequest>
    {
        public DailyCaloriesRequestValidator()
        {
            RuleFor(x => x.DailyCalories)
                .GreaterThan(0).WithMessage("A meta de calorias diárias deve ser maior que zero.")
                .LessThanOrEqualTo(10000).WithMessage("A meta de calorias diárias não pode ultrapassar 10.000 calorias.");

            RuleFor(x => x.WeightGoal)
                .IsInEnum().WithMessage("O objetivo deve ser um valor válido.");
        }
    }
}
