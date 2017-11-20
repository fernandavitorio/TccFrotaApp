using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;


namespace TccFrotaApp.ViewModels
{
    [Validator(typeof(VeiculoViewModelValidator))]
    public class VeiculoViewModel
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public string Identificador { get; set; }

    }
}