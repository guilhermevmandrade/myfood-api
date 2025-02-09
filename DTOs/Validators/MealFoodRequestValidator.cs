using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class MealFoodRequestValidator : AbstractValidator<MealFoodRequest>
    {
        public MealFoodRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(x => x.Unit)
                .IsInEnum().WithMessage("Unidade de medida inválida.");
        }
    }
}
