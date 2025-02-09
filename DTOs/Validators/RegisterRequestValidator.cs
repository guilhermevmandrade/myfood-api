using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome pode ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("E-mail inválido.")
                .MaximumLength(100).WithMessage("O e-mail pode ter no máximo 100 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.")
                .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
                .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
                .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
                .Matches(@"[\@\!\#\$\%\^\&\*\(\)\+\-\=]").WithMessage("A senha deve conter pelo menos um caractere especial (@, !, #, $, etc.).");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("O gênero deve ser um valor válido.");

            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("A idade deve ser maior que zero.")
                .LessThanOrEqualTo(100).WithMessage("A idade deve ser um valor realista.");

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
