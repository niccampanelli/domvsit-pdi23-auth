using Application.Authentication.Boundaries.SignUp;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class SignUpCommand : Command<SignUpOutput>
    {
        public SignUpInput Input { get; set; }

        public SignUpCommand(SignUpInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new SignUpCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
