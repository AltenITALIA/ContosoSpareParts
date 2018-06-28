using System;
using Humanizer;
using MassTransit;

namespace SpareParts.Part.DomainModel
{
    public class History
    {
        private string _photoUri;

        public History(string id, string partCode, string vehicleId)
        {
            if (string.IsNullOrWhiteSpace(partCode)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(partCode));
            if (string.IsNullOrWhiteSpace(vehicleId)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(vehicleId));
            if (partCode.Length > 255) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(partCode));
            if (vehicleId.Length > 36) throw new ArgumentException("Value cannot exceed 36 characters.", nameof(vehicleId));
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));
            Id = id;
            PartCode = partCode;
            VehicleId = vehicleId;
            Date = DateTime.Now;
        }

        public string Id { get; private set; }

        public string PartCode { get; private set; }

        public string VehicleId { get; private set; }

        public DateTime Date { get; set; }

        public string PhotoUri
        {
            get => _photoUri;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
                if (value.Length > 1000) throw new ArgumentException("Value cannot exceed 1000 characters.", nameof(value));

                _photoUri = value;
            }
        }
    }
}
