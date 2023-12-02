using Application.Authentication.Boundaries.ResetPassword;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class ResetPasswordCommandValidation : AbstractValidator<ResetPasswordInput>
    {
        public ResetPasswordCommandValidation()
        {
            RuleFor(i => i.Login)
                .NotEmpty().WithMessage("A string de login deve ser informada")
                .NotNull().WithMessage("A string de login deve ser informada");
            RuleFor(i => i.OldPassword)
                .NotEmpty().WithMessage("A senha atual deve ser informada")
                .NotNull().WithMessage("A senha atual deve ser informada");
            RuleFor(i => i.NewPassword)
                .NotEmpty().WithMessage("A nova senha à ser definida deve ser informada")
                .NotNull().WithMessage("A nova senha à ser definida deve ser informada")
                .MinimumLength(8).WithMessage("A nova senha à ser definida deve conter no mínimo 8 caracteres")
                .Matches(@"[\p{Lu}]+").WithMessage("A nova senha à ser definida deve conter ao menos uma letra maiúscula")
                .Matches(@"[\p{Ll}]+").WithMessage("A nova senha à ser definida deve conter ao menos uma letra minúscula")
                .Matches(@"[0-9]+").WithMessage("A nova senha à ser definida deve conter ao menos um número")
                .Matches(@"[^\p{Lu}\p{Ll}0-9]").WithMessage("A nova senha à ser definida deve conter ao menos um caractere especial")
                .NotEqual(i => i.OldPassword).WithMessage("A nova senha não deve ser igual à senha atual");
        }
    }
}
