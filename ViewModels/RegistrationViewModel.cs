using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;

namespace TccFrotaApp.ViewModels 
{
    [Validator(typeof(RegistrationViewModelValidator))]
    public class RegistrationViewModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }
              
    }
}