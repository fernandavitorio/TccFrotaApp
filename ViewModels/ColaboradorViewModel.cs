using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;

namespace TccFrotaApp.ViewModels 
{
    [Validator(typeof(ColaboradorViewModelValidator))]
    public class ColaboradorViewModel 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Matricula { get; set; }
        public string Funcao { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
              
    }
}