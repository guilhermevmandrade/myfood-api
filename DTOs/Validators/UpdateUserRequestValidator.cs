using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres.");

            RuleFor(x => x.Height)
                .GreaterThan(0).WithMessage("A altura deve ser um valor positivo.")
                .LessThanOrEqualTo(250).WithMessage("A altura deve ser um valor realista (máximo 250 cm).");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("O peso deve ser um valor positivo.")
                .LessThanOrEqualTo(500).WithMessage("O peso deve ser um valor realista (máximo 500 kg).");

            RuleFor(x => x.ActivityLevel)
                .IsInEnum().WithMessage("O nível de atividade deve ser um valor válido.");
        }
    }
}
