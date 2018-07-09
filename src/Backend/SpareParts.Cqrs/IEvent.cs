namespace SpareParts.Cqrs
{
    public interface IEvent : IMessage
    {
    }

    public abstract class EventBase : IEvent
    {
    }
}
