using FluentValidation;
using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;

namespace TccFrotaApp.ViewModels.Validations
{
    public class ColaboradorViewModelValidator : AbstractValidator<ColaboradorViewModel>
    {
        public ColaboradorViewModelValidator()
        {
            RuleFor(vm => vm.Nome).NotEmpty().WithMessage("Informe o nome");
            RuleFor(vm => vm.Matricula).NotEmpty().WithMessage("Informe o número da matricula");
            RuleFor(vm => vm.Funcao).NotEmpty().WithMessage("Informe a função");
            RuleFor(vm => vm.Email).NotEmpty().WithMessage("Colaborador terá acesso ao sistema, informe o email").When(vm => vm.Funcao != TIPO_COLABORADOR.COLETOR.ToString());
            RuleFor(vm => vm.Senha).NotEmpty().WithMessage("Colaborador terá acesso ao sistema, informe a senha").When(vm => vm.Funcao != TIPO_COLABORADOR.COLETOR.ToString());
        }
    }

}
