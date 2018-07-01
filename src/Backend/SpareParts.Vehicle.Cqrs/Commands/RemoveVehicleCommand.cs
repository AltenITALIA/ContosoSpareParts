using System;
using SpareParts.Cqrs;

namespace SpareParts.Vehicle.Cqrs.Commands
{
    public class RemoveVehicleCommand : CommandBase
    {
        public RemoveVehicleCommand(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public string Id { get; }
    }
}
