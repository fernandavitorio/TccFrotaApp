using FluentValidation.Attributes;
using TccFrotaApp.ViewModels.Validations;


namespace TccFrotaApp.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}