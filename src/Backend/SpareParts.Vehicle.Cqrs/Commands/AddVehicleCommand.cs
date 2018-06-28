using System;
using SpareParts.Cqrs;

namespace SpareParts.Vehicle.Cqrs.Commands
{
    public class AddVehicleCommand : CommandBase
    {
        public AddVehicleCommand(string id, string brand, string customer, string plate, string model, string color, long year)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Plate = plate ?? throw new ArgumentNullException(nameof(plate));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Color = color ?? throw new ArgumentNullException(nameof(color));
            Year = year;
        }

        public string Brand { get; }

        public string Customer { get; }

        public string Plate { get; }

        public string Model { get; }

        public string Color { get; }

        public long Year { get; }

        public string Id { get; }
    }
}
