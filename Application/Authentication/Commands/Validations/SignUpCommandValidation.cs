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
                .NotNull().WithMessage("A senha deve ser informada")
                .MinimumLength(8).WithMessage("A senha deve conter no mínimo 8 caracteres")
                .Matches(@"[\p{Lu}]+").WithMessage("A senha deve conter ao menos uma letra maiúscula")
                .Matches(@"[\p{Ll}]+").WithMessage("A senha deve conter ao menos uma letra minúscula")
                .Matches(@"[0-9]+").WithMessage("A senha deve conter ao menos um número")
                .Matches(@"[^\p{Lu}\p{Ll}0-9]").WithMessage("A senha deve conter ao menos um caractere especial");
        }
    }
}
