using Application.Authentication.Boundaries.RevalidateToken;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class RevalidateTokenCommandValidation : AbstractValidator<RevalidateTokenInput>
    {
        public RevalidateTokenCommandValidation()
        {
            RuleFor(i => i.Authorization)
                .NotEmpty().WithMessage("Usuário não autenticado. O token de autenticação precisa estar presente")
                .NotNull().WithMessage("Usuário não autenticado. O token de autenticação precisa estar presente");
        }
    }
}
