using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.User;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public ResetPasswordHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<ResetPasswordOutput> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                input.Login = input.Login.ToLower().Trim();
                input.OldPassword = _authenticationUseCase.EncryptPassword(input.OldPassword);
                input.NewPassword = _authenticationUseCase.EncryptPassword(input.NewPassword);

                var authenticateInput = new AuthenticateDto()
                {
                    Email = input.Login,
                    EncryptedPassword = input.OldPassword
                };

                var authenticatedUser = await _authenticationUseCase.Authenticate(authenticateInput);

                if (authenticatedUser == null || authenticatedUser.Id <= 0L)
                {
                    var message = "Login ou senha inválidos";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                await _authenticationUseCase.RemoveRegisteredUserRefreshTokens(authenticatedUser.Id);

                var setNewPasswordInput = new PasswordDto()
                {
                    UserId = authenticatedUser.Id,
                    EncryptedPassword = input.NewPassword
                };

                await _authenticationUseCase.SetNewPassword(setNewPasswordInput);
                return default;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
