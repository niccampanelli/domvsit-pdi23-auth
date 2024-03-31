using Application.Authentication.Boundaries.ExtractIdFromToken;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class ExtractIdFromTokenCommandValidation : AbstractValidator<ExtractIdFromTokenInput>
    {
        public ExtractIdFromTokenCommandValidation()
        {
            RuleFor(i => i.Authorization)
                .NotEmpty().WithMessage("Participante não autenticado. O token de autenticação precisa estar presente")
                .NotNull().WithMessage("Participante não autenticado. O token de autenticação precisa estar presente")
                .Matches(@"\bBearer\b").WithMessage("Participante não autenticado. Token de autenticação inválido");
        }
    }
}
