using Application.Authentication.Boundaries.ValidateAuthentication;
using Application.Authentication.Commands;
using Application.UseCase.Authentication;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class ValidateAuthenticationHandler : IRequestHandler<ValidateAuthenticationCommand, ValidateAuthenticationOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IAuthenticationUseCase _authenticationUseCase;

        public ValidateAuthenticationHandler(IMediatorHandler mediatorHandler, IAuthenticationUseCase authenticationUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _authenticationUseCase = authenticationUseCase;
        }

        public async Task<ValidateAuthenticationOutput> Handle(ValidateAuthenticationCommand command, CancellationToken cancellationToken)
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
