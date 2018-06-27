using System;
using Humanizer;
using MassTransit;

namespace SpareParts.Vehicle.DomainModel
{
    public class Movie
    {
        private string _title;
        private int _year;

        public Movie(string title) : this(NewId.Next().ToString("D"), title)
        {
        }

        public Movie(string id, string title)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(id));
            Id = id;
            Title = title;
            Year = 1900;
        }

        public string Id { get; private set; }

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
                if (value.Length > 255) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(value));

                _title = value.Titleize();
            }
        }

        public int Year
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
