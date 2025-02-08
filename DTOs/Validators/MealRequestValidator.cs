using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class MealRequestValidator : AbstractValidator<MealRequest>
    {
        public MealRequestValidator()
        {
            RuleFor(meal => meal.Description)
                .NotEmpty().WithMessage("A descrição da refeição é obrigatória.")
                .MaximumLength(255).WithMessage("A descrição da refeição não pode ter mais de 255 caracteres.");

            RuleFor(meal => meal.MealTime)
                .NotEmpty().WithMessage("A data e horário da refeição são obrigatórios.");
        }
    }
}
