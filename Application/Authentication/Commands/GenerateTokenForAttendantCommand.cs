using Application.Authentication.Boundaries.GenerateTokenForAttendant;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class GenerateTokenForAttendantCommand : Command<GenerateTokenForAttendantOutput>
    {
        public GenerateTokenForAttendantInput Input { get; set; }

        public GenerateTokenForAttendantCommand(GenerateTokenForAttendantInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new GenerateTokenForAttendantCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
