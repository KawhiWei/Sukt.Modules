using MediatR;

namespace Sukt.Module.Core.Events
{
    public abstract class NotificationHandlerBase<TEvent> : EventHandlerBase<TEvent>, INotificationHandler<TEvent> where TEvent : EventBase
    {
    }
}