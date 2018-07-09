using SpareParts.Cqrs;

namespace SpareParts.Vehicle.Cqrs.Events
{
    public class VehicleAddedEvent : EventBase
    {
        public string VehicleId { get; set; }
    }
}
