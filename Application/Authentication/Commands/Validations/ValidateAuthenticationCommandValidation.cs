using Application.Authentication.Boundaries.ValidateAuthentication;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class ValidateAuthenticationCommandValidation : AbstractValidator<ValidateAuthenticationInput>
    {
        public ValidateAuthenticationCommandValidation()
        {
            RuleFor(i => i.Authorization)
                .NotEmpty().WithMessage("Usuário não autenticado. O token de autenticação precisa estar presente")
                .NotNull().WithMessage("Usuário não autenticado. O token de autenticação precisa estar presente")
                .Matches(@"\bBearer \b").WithMessage("Usuário não autenticado. Token de autenticação inválido");
        }
    }
}
