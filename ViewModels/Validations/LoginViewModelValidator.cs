using FluentValidation;

namespace TccFrotaApp.ViewModels.Validations
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(vm => vm.UserName).NotEmpty().WithMessage("Informe o nome do usuário");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Informe a senha");     
        }
    }
}