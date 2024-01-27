using Application.Authentication.Boundaries.RestoreUserData;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class RestoreUserDataCommandValidation : AbstractValidator<RestoreUserDataInput>
    {
        public RestoreUserDataCommandValidation()
        {
            RuleFor(i => i.Authorization)
                .NotEmpty().WithMessage("Usuário não autenticado. O token de autenticação precisa estar presente")
                .NotNull().WithMessage("Usuário não autenticado. O token de autenticação precisa estar presente")
                .Matches(@"\bBearer\b").WithMessage("Usuário não autenticado. Token de autenticação inválido");
        }
    }
}
