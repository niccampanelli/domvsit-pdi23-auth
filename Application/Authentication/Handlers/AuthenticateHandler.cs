using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using Domain.Dto.User;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, AuthenticateOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public AuthenticateHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<AuthenticateOutput> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {
                var input = command.Input;

                input.Login = input.Login.ToLower();
                input.Password = _authenticationUseCase.EncryptPassword(input.Password);

                var authenticatedUser = await _authenticationUseCase.Authenticate(input.Login, input.Password);

                if (authenticatedUser.Id <= 0L)
                {
                    var message = "Login ou senha inválidos";
                    await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, message));
                    return default;
                }

                var token = _authenticationUseCase.GenerateToken(authenticatedUser.Id);
                var refreshToken = _authenticationUseCase.GenerateRefreshToken();

                var registerRefreshTokenInput = new RefreshTokenDto()
                {
                    UserId = authenticatedUser.Id,
                    Value = refreshToken
                };

                await _authenticationUseCase.RegisterRefreshTokenSession(registerRefreshTokenInput);

                var result = new AuthenticateOutput()
                {
                    Id = authenticatedUser.Id,
                    Email = authenticatedUser.Email,
                    Name = authenticatedUser.Name,
                    Token = token,
                    RefreshToken = refreshToken
                };

                return result;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
