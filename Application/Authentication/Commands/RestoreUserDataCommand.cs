using Application.Authentication.Boundaries.RestoreUserData;
using Application.Authentication.Commands.Validations;
using Domain.Base.Messages;

namespace Application.Authentication.Commands
{
    public class RestoreUserDataCommand : Command<RestoreUserDataOutput>
    {
        public RestoreUserDataInput Input { get; set; }

        public RestoreUserDataCommand(RestoreUserDataInput input)
        {
            Input = input;
        }

        public override bool IsValid()
        {
            ValidationResult = new RestoreUserDataCommandValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
