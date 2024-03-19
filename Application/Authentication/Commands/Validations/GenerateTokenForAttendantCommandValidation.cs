using Application.Authentication.Boundaries.GenerateTokenForAttendant;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class GenerateTokenForAttendantCommandValidation : AbstractValidator<GenerateTokenForAttendantInput>
    {
        public GenerateTokenForAttendantCommandValidation()
        {
            RuleFor(i => i.AttendantId)
                .NotEmpty().WithMessage("Informe o id do participante")
                .NotNull().WithMessage("Informe o id do participante")
                .GreaterThan(0).WithMessage("Informe o id do participante");
        }
    }
}
