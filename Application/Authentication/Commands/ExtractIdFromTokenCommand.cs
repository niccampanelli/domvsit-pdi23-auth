using Application.Authentication.Boundaries.ExtractIdFromToken;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class ExtractIdFromTokenCommand : Command<ExtractIdFromTokenOutput>
    {
        public ExtractIdFromTokenInput Input { get; set; }

        public ExtractIdFromTokenCommand(ExtractIdFromTokenInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new ExtractIdFromTokenCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
