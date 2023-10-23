using Application.Authentication.Boundaries.ResetPassword;
using Application.Authentication.Commands;
using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Application.Authentication.Handlers
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ResetPasswordOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ResetPasswordHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public async Task<ResetPasswordOutput> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
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
