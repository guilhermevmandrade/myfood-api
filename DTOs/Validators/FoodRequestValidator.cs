using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.Validators
{
    public class FoodRequestValidator : AbstractValidator<FoodRequest>
    {
        public FoodRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do alimento é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do alimento pode ter no máximo 100 caracteres.");

            RuleFor(x => x.Calories)
                .GreaterThanOrEqualTo(0).WithMessage("As calorias não podem ser negativas.");

            RuleFor(x => x.Proteins)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade de proteína não pode ser negativa.");

            RuleFor(x => x.Carbs)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade de carboidratos não pode ser negativa.");

            RuleFor(x => x.Fats)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade de gorduras não pode ser negativa.");
        }
    }
}
