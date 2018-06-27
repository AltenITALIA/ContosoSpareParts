using System;
using Humanizer;
using MassTransit;

namespace SpareParts.Vehicle.DomainModel
{
    public class Vehicle
    {
        private string _brand;
        private string _color;
        private string _model;
        private string _customer;
        private string _plate;
        private long _year;

        public Vehicle() : this(NewId.Next().ToString("D"))
        {
        }

        public Vehicle(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));
            Id = id;
            Year = 1900;
        }

        public string Id { get; private set; }

        public string Brand
        {
            get => _brand;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
                if (value.Length > 255) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(value));

                _brand = value.Titleize();
            }
        }

        public string Model
        {
            get => _model;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
                if (value.Length > 255) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(value));

                _model = value.Titleize();
            }
        }

        public string Customer
        {
            get => _customer;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
                if (value.Length > 255) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(value));

                _customer = value.Titleize();
            }
        }

        public string Plate
        {
            get => _plate;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
                if (value.Length > 20) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(value));

                _plate = value.Titleize();
            }
        }

        public string Color
        {
            get => _color;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
                if (value.Length > 20) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(value));

                _color = value.Titleize();
            }
        }
        public long Year
        {
            get => _year;
            set
            {
                if (value < 1900) throw new ArgumentException($"{nameof(Year)} cannot be less than 1900");
                _year = value;
            }
        }
    }
}
