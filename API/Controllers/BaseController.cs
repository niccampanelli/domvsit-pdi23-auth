using Domain.Base.Communication.Mediator;
using Domain.Base.Messages.Common.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notificationHandler;
        private readonly IMediatorHandler _mediatorHandler;

        protected BaseController(INotificationHandler<DomainNotification> notificationHandler, IMediatorHandler mediatorHandler)
        {
            _notificationHandler = (DomainNotificationHandler) notificationHandler;
            _mediatorHandler = mediatorHandler;
        }

        protected bool IsValidOperation()
        {
            return !_notificationHandler.HasNotifications();
        }

        protected IEnumerable<string> GetMessages()
        {
            return _notificationHandler.GetNotifications().Select(x => x.Value).ToList();
        }
    }
}
