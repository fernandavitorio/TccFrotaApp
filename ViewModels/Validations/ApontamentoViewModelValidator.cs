using FluentValidation;

namespace TccFrotaApp.ViewModels.Validations
{
    public class ApontamentoViewModelValidator : AbstractValidator<ApontamentoViewModel>
    {
        public ApontamentoViewModelValidator()
        {
            RuleFor(vm => vm.Setor).NotEmpty().WithMessage("Informe o setor do apontamento").When(a => a.Tipo == TIPO_APONTAMENTO.INICIAL.ToString());
            RuleFor(vm => vm.Tipo).NotEmpty().WithMessage("Informe o tipo do apontamento");
            RuleFor(vm => vm.VeiculoId).GreaterThan(0).WithMessage("Apontamento deve ter um veiculo");
            RuleFor(vm => vm.MotoristaId).GreaterThan(0).WithMessage("Apontamento deve ter um motorista").When(a => a.Tipo == TIPO_APONTAMENTO.INICIAL.ToString());
            RuleFor(vm => vm.Coletor1Id).GreaterThan(0).WithMessage("Apontamento deve ter no mínimo um coletor").When(a => a.Tipo == TIPO_APONTAMENTO.INICIAL.ToString());
        }
    }
}