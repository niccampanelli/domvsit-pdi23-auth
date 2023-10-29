using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
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
                await _authenticationUseCase.Create(new Domain.Dto.User.UserDto()
                {
                    Name = "Teste",
                    Email = "teste@email.com",
                    Password = "teste"
                });
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
