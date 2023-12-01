using Application.Authentication.Boundaries.RevalidateToken;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class RevalidateTokenCommand : Command<RevalidateTokenOutput>
    {
        public RevalidateTokenInput Input { get; set; }

        public RevalidateTokenCommand(RevalidateTokenInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new RevalidateTokenCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
