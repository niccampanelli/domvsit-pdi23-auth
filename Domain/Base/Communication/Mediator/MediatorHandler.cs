using Domain.Base.Messages;
using Domain.Base.Messages.Common.Notification;
using MediatR;

namespace Domain.Base.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task<TResponse> SendCommand<TCommand, TResponse>(TCommand command) where TCommand : Command<TResponse>
        {
            return await _mediator.Send(command);
        }
    }
}
