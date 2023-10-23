using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class AuthenticateCommand : Command<AuthenticateOutput>
    {
        public AuthenticateInput Input { get; set; }

        public AuthenticateCommand(AuthenticateInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new AuthenticateCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
