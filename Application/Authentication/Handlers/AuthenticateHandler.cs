using Application.Authentication.Boundaries.Authenticate;
using Application.Authentication.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateCommand, AuthenticateOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public AuthenticateHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<AuthenticateOutput> Handle(AuthenticateCommand command, CancellationToken cancellationToken)
        {
            if (command.IsValid())
            {

            }

            foreach (var error in command.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return default;
        }
    }
}
