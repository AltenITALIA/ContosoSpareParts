namespace SpareParts.Cqrs.Events
{
    public class CompletionTimeoutEvent<T> : EventBase
    {
        public T Data { get; }

        public CompletionTimeoutEvent(T data)
        {
            Data = data;
        }
    }
}
