using SpareParts.Cqrs;

namespace SpareParts.Part.Cqrs.Events
{
    public class PartAddedEvent : EventBase
    {
        public string PartCode { get; set; }
    }
}
