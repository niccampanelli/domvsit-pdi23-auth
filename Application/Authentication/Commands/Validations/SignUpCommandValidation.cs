using Application.Authentication.Boundaries.SignUp;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class SignUpCommandValidation : AbstractValidator<SignUpInput>
    {
        public SignUpCommandValidation()
        {
            RuleFor(i => i.Name)
                .NotEmpty().WithMessage("O nome deve ser informado")
                .NotNull().WithMessage("O nome deve ser informado");
            RuleFor(i => i.Email)
                .NotEmpty().WithMessage("O endereço de email deve ser informado")
                .NotNull().WithMessage("O endereço de email deve ser informado")
                .EmailAddress().WithMessage("Informe um endereço de email válido");
            RuleFor(i => i.Password)
                .NotEmpty().WithMessage("A senha deve ser informada")
                .NotNull().WithMessage("A senha deve ser informada");
        }
    }
}
