using System;
using SpareParts.Cqrs;

namespace SpareParts.Part.Cqrs.Commands
{
    public class AddHistoryCommand : CommandBase
    {
        public AddHistoryCommand(string id, string partCode, string vehicleId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            PartCode = partCode ?? throw new ArgumentNullException(nameof(partCode));
            VehicleId = vehicleId ?? throw new ArgumentNullException(nameof(vehicleId));          
        }

        public string VehicleId { get; }

        public string Id { get; }

        public string PartCode { get; }
    }
}
