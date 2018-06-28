using System;
using Humanizer;
using MassTransit;

namespace SpareParts.Part.DomainModel
{
    public class Part
    {
        private string _photoUri;

        public Part(string code, string name)
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(code));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            if (code.Length > 255) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(code));
            if (name.Length > 255) throw new ArgumentException("Value cannot exceed 255 characters.", nameof(name));
            Code = code;
            Name = name;
        }

        public string Code { get; private set; }

        public string Name { get; private set; }

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
