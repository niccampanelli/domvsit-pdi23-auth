using Application.Authentication.Boundaries.ValidateAuthentication;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class ValidateAuthenticationCommand : Command<ValidateAuthenticationOutput>
    {
        public ValidateAuthenticationInput Input { get; set; }

        public ValidateAuthenticationCommand(ValidateAuthenticationInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new ValidateAuthenticationCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
