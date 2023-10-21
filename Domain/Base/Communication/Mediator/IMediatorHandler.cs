using Domain.Base.Messages;
using Domain.Base.Messages.Common.Notification;

namespace Domain.Base.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task<TResponse> SendCommand<TCommand, TResponse>(TCommand command) where TCommand : Command<TResponse>;
    }
}
