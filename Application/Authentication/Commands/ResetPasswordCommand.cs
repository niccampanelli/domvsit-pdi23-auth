using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class ResetPasswordCommand : Command<ResetPasswordOutput>
    {
        public ResetPasswordInput Input { get; set; }

        public ResetPasswordCommand(ResetPasswordInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new ResetPasswordCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
