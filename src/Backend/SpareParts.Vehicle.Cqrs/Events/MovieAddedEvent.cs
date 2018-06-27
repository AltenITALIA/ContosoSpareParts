using SpareParts.Cqrs;

namespace SpareParts.Vehicle.Cqrs.Events
{
    public class MovieAddedEvent : EventBase
    {
        public string MovieId { get; set; }
    }
}
