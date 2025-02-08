using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.");
        }
    }
}
