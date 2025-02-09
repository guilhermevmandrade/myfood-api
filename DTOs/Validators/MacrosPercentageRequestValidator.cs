using FluentValidation;
using MyFood.DTOs.Requests;

namespace MyFood.DTOs.Validators
{
    public class MacrosPercentageRequestValidator : AbstractValidator<MacrosPercentageRequest>
    {
        public MacrosPercentageRequestValidator()
        {
            RuleFor(x => x.ProteinsPercentage)
                .InclusiveBetween(0, 100).WithMessage("O percentual de proteínas deve estar entre 0 e 100.");

            RuleFor(x => x.CarbsPercentage)
                .InclusiveBetween(0, 100).WithMessage("O percentual de carboidratos deve estar entre 0 e 100.");

            RuleFor(x => x.FatsPercentage)
                .InclusiveBetween(0, 100).WithMessage("O percentual de gorduras deve estar entre 0 e 100.");

            RuleFor(x => x)
                .Must(x => x.ProteinsPercentage + x.CarbsPercentage + x.FatsPercentage == 100)
                .WithMessage("A soma dos percentuais de proteínas, carboidratos e gorduras deve ser igual a 100.");
        }
    }
}
