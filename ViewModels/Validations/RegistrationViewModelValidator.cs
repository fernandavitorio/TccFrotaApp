using FluentValidation;
using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;

namespace TccFrotaApp.ViewModels.Validations
{
    public class RegistrationViewModelValidator : AbstractValidator<RegistrationViewModel>
    {
        public RegistrationViewModelValidator()
        {
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Informe o email");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Informe a senha");
        }
    }
}
