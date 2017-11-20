using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;


namespace TccFrotaApp.ViewModels
{
    [Validator(typeof(LoginViewModelValidator))]
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}