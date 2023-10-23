using Application.Authentication.Boundaries.Authenticate;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class AuthenticateCommandValidation : AbstractValidator<AuthenticateInput>
    {
        public AuthenticateCommandValidation()
        {
            RuleFor(i => i.Login)
                .NotEmpty().WithMessage("A string de login deve ser informada")
                .NotNull().WithMessage("A string de login deve ser informada");
            RuleFor(i => i.Password)
                .NotEmpty().WithMessage("A senha deve ser informada")
                .NotNull().WithMessage("A senha deve ser informada");
        }
    }
}
