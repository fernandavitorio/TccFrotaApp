using FluentValidation;

namespace TccFrotaApp.ViewModels.Validations
{
    public class ApontamentoViewModelValidator : AbstractValidator<ApontamentoViewModel>
    {
        public ApontamentoViewModelValidator()
        {
            RuleFor(vm => vm.Setor).NotEmpty().WithMessage("Informe o setor do apontamento");
            RuleFor(vm => vm.Tipo).NotEmpty().WithMessage("Informe o tipo do apontamento");
            RuleFor(vm => vm.VeiculoId).LessThanOrEqualTo(0).WithMessage("Apontamento deve ter um veiculo");
            RuleFor(vm => vm.MotoristaId).LessThanOrEqualTo(0).WithMessage("Apontamento deve ter um motorista");
            RuleFor(vm => vm.Coletor1Id).LessThanOrEqualTo(0).WithMessage("Apontamento deve ter no mínimo um coletor");


        }
    }
}