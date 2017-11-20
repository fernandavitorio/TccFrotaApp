using FluentValidation;

namespace TccFrotaApp.ViewModels.Validations
{
    public class VeiculoViewModelValidator : AbstractValidator<VeiculoViewModel>
    {
        public VeiculoViewModelValidator()
        {
            RuleFor(vm => vm.Marca).NotEmpty().WithMessage("Informe a marca do veiculo");
            RuleFor(vm => vm.Modelo).NotEmpty().WithMessage("Informe o modelo do veiculo");
            RuleFor(vm => vm.Identificador).NotEmpty().WithMessage("Informe a identificação do veiculo");
            RuleFor(vm => vm.Placa).NotEmpty().WithMessage("Informe a placa do veiculo");
        }
    }
}