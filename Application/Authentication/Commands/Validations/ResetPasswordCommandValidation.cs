using Application.Authentication.Boundaries.ResetPassword;
using FluentValidation;

namespace Application.Authentication.Commands.Validations
{
    public class ResetPasswordCommandValidation : AbstractValidator<ResetPasswordInput>
    {
        public ResetPasswordCommandValidation()
        {
            RuleFor(i => i.OldPassword)
                .NotEmpty().WithMessage("A senha atual deve ser informada")
                .NotNull().WithMessage("A senha atual deve ser informada");
            RuleFor(i => i.NewPassword)
                .NotEmpty().WithMessage("A nova senha a ser definida deve ser informada")
                .NotNull().WithMessage("A nova senha a ser definida deve ser informada");
        }
    }
}
