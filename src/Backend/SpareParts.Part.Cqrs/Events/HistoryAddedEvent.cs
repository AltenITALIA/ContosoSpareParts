using SpareParts.Cqrs;

namespace SpareParts.Part.Cqrs.Events
{
    public class HistoryAddedEvent : EventBase
    {
        public string PartCode { get; set; }

        public string VehicleId { get; set; }
    }
}
